using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.TextGeneration;
using EnglishTutorAI.Domain.Entities;
using EnglishTutorAI.Domain.Enums;
using Microsoft.Extensions.Options;

namespace EnglishTutorAI.Application.Services
{
    [ScopedDependency]
    public class TextCorrectionService : ITextCorrectionService
    {
        private readonly string _assistantId;
        private readonly IAssistantClientService _assistantClientService;
        private readonly ITextComparisonService _textComparisonService;
        private readonly ITextExtractionService _textExtractionService;
        private readonly ITextCorrectionMessageGenerationService _messageGenerationService;
        private readonly ISingleEntryCache _singleEntryCache;
        private readonly IRepository<LinguaFixMessage> _linguaFixMessageRepository;

        public TextCorrectionService(
            IAssistantClientService assistantClientService,
            IOptionsMonitor<OpenAiConfig> openAiConfig,
            ITextComparisonService textComparisonService,
            ITextExtractionService textExtractionService,
            ITextCorrectionMessageGenerationService messageGenerationService,
            ISingleEntryCache singleEntryCache,
            IRepository<LinguaFixMessage> linguaFixMessageRepository)
        {
            _assistantClientService = assistantClientService;
            _textComparisonService = textComparisonService;
            _textExtractionService = textExtractionService;
            _messageGenerationService = messageGenerationService;
            _singleEntryCache = singleEntryCache;
            _linguaFixMessageRepository = linguaFixMessageRepository;
            _assistantId = openAiConfig.CurrentValue.EnglishFixerAssistantId!;
        }

        public async Task<TextCorrectionResult> Correct(TextGenerationRequest request)
        {
            var correctedText = _singleEntryCache.Get(request.OriginalText);

            if (correctedText == null)
            {
                await GenerateAndAddUserMessageAsync(request);
                await _assistantClientService.CreateRunRequest(_assistantId, request.ThreadId);
                correctedText = await GenerateCorrectedMessageAsync(request);
                _singleEntryCache.Set(request.OriginalText, correctedText);
            }

            var isCorrected = _textComparisonService.HasTextChanged(request.TranslatedText, correctedText);

            if (isCorrected)
            {
                await _linguaFixMessageRepository.Add(new LinguaFixMessage
                {
                    ThreadId = request.ThreadId,
                    TranslatedText = request.TranslatedText,
                    CorrectedText = correctedText,
                    DocumentId = request.DocumentId,
                    SessionId = request.SessionId
                });
            }

            return new TextCorrectionResult(correctedText, isCorrected);
        }

        private async Task GenerateAndAddUserMessageAsync(TextGenerationRequest request)
        {
            var message = await _messageGenerationService.GenerateMessageAsync(request);
            await _assistantClientService.AddMessageToThread(request.ThreadId, message);
        }

        private async Task<string> GenerateCorrectedMessageAsync(TextGenerationRequest request)
        {
            var correctedText = await _assistantClientService.GetLastMessage(request.ThreadId);
            var cleanCorrectedText = _textExtractionService.ExtractCleanText(correctedText, request.OriginalText);

            return cleanCorrectedText;
        }
    }
}