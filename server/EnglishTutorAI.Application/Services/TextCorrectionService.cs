using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.TextGeneration;
using EnglishTutorAI.Domain.Enums;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using OpenAI.Assistants;

namespace EnglishTutorAI.Application.Services
{
    [ScopedDependency]
    public class TextCorrectionService : ITextCorrectionService
    {
        private readonly string _assistantId;
        private readonly IAssistantClientService _assistantClientService;
        private readonly ITextComparisonService _textComparisonService;
        private readonly ITextExtractionService _textExtractionService;
        private readonly IChatMessageAddService _chatMessageAddService;
        private readonly ITextCorrectionMessageGenerationService _messageGenerationService;
        private readonly ISingleEntryCache _singleEntryCache;

        public TextCorrectionService(
            IAssistantClientService assistantClientService,
            IOptionsMonitor<OpenAiConfig> openAiConfig,
            ITextComparisonService textComparisonService,
            ITextExtractionService textExtractionService,
            IChatMessageAddService chatMessageAddService,
            ITextCorrectionMessageGenerationService messageGenerationService,
            ISingleEntryCache singleEntryCache)
        {
            _assistantClientService = assistantClientService;
            _textComparisonService = textComparisonService;
            _textExtractionService = textExtractionService;
            _chatMessageAddService = chatMessageAddService;
            _messageGenerationService = messageGenerationService;
            _singleEntryCache = singleEntryCache;
            _assistantId = openAiConfig.CurrentValue.EnglishFixerAssistantId!;
        }

        public async Task<TextCorrectionResult> Correct(TextGenerationRequest request)
        {
            var correctedText = _singleEntryCache.Get(request.OriginalText);

            if (correctedText == null)
            {
                await GenerateAndAddUserMessageAsync(request);
                await _assistantClientService.CreateRunRequest(_assistantId, request.ThreadId);
                correctedText = await GenerateCorrectedMessageAsync(request.ThreadId, request.TranslatedText);

                _singleEntryCache.Set(request.OriginalText, correctedText);
            }

            var isCorrected = _textComparisonService.HasTextChanged(request.TranslatedText, correctedText);

            return new TextCorrectionResult(correctedText, isCorrected);
        }

        private async Task GenerateAndAddUserMessageAsync(TextGenerationRequest request)
        {
            var message = await _messageGenerationService.GenerateMessageAsync(request);
            await _assistantClientService.AddMessageToThread(request.ThreadId, message);
            await _chatMessageAddService.Add(new AddChatMessageModel(
                request.ThreadId, request.TranslatedText, ChatType.TextCorrection, ConversationRole.User));
        }

        private async Task<string> GenerateCorrectedMessageAsync(string threadId, string originalText)
        {
            var correctedText = await _assistantClientService.GetLastMessage(threadId);
            var cleanCorrectedText = _textExtractionService.ExtractCleanText(correctedText, originalText);

            await _chatMessageAddService.Add(new AddChatMessageModel(
                threadId, cleanCorrectedText, ChatType.TextCorrection, ConversationRole.Assistant));

            return cleanCorrectedText;
        }
    }
}