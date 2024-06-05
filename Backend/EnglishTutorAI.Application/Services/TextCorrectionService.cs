using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.TextGeneration;
using EnglishTutorAI.Domain.Enums;
using Microsoft.Extensions.Options;
using OpenAI.Threads;

namespace EnglishTutorAI.Application.Services
{
    public class TextCorrectionService : ITextCorrectionService
    {
        private readonly string _assistantId;
        private readonly IAssistantClient _assistantClient;
        private readonly ITextComparisonService _textComparisonService;
        private readonly ITextExtractionService _textExtractionService;
        private readonly IChatMessageAddService _chatMessageAddService;
        private readonly ITextCorrectionMessageGenerationService _messageGenerationService;

        public TextCorrectionService(
            IAssistantClient assistantClient,
            IOptionsMonitor<OpenAiConfig> openAiConfig,
            ITextComparisonService textComparisonService,
            ITextExtractionService textExtractionService,
             IChatMessageAddService chatMessageAddService,
            ITextCorrectionMessageGenerationService messageGenerationService)
        {
            _assistantClient = assistantClient;
            _textComparisonService = textComparisonService;
            _textExtractionService = textExtractionService;
            _chatMessageAddService = chatMessageAddService;
            _messageGenerationService = messageGenerationService;
            _assistantId = openAiConfig.CurrentValue.EnglishTutorAssistantId!;
        }

        public async Task<(bool IsCorrected, string CorrectedText)> Correct(TextGenerationRequest request)
        {
            await GenerateAndAddUserMessageAsync(request);
            var runResponse = await CreateAndValidateRunRequestAsync(request.ThreadId);
            var correctedText = await GenerateCorrectedMessageAsync(runResponse, request.ThreadId, request.TranslatedText);
            var isCorrected = _textComparisonService.HasTextChanged(request.TranslatedText, correctedText);

            return (isCorrected, correctedText);
        }

        private async Task GenerateAndAddUserMessageAsync(TextGenerationRequest request)
        {
            var message = await _messageGenerationService.GenerateMessageAsync(request);
            await _assistantClient.AddMessageToThread(request.ThreadId, message);
            await _chatMessageAddService.Add(new AddChatMessageModel(
                request.ThreadId, request.TranslatedText, ChatType.TextCorrection, ConversationRole.User));
        }

        private async Task<RunResponse> CreateAndValidateRunRequestAsync(string threadId)
        {
            var runResponse = await _assistantClient.CreateRunRequest(_assistantId, threadId);

            if (runResponse.Status != RunStatus.Completed)
            {
                throw new InvalidOperationException("The text correction run did not complete successfully.");
            }

            return runResponse;
        }

        private async Task<string> GenerateCorrectedMessageAsync(
            RunResponse runResponse,
            string threadId,
            string originalText)
        {
            var correctedText = await _assistantClient.GenerateLastMessage(
                new GenerateLastMessageModel(runResponse, threadId, ChatType.TextCorrection));
            var cleanCorrectedText = _textExtractionService.ExtractCleanText(correctedText, originalText);

            await _chatMessageAddService.Add(new AddChatMessageModel(
                threadId, cleanCorrectedText, ChatType.TextCorrection, ConversationRole.Assistant));

            return cleanCorrectedText;
        }
    }
}