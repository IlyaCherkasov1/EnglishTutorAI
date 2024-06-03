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
        private const string TranslatedTextPlaceholder = "{TranslatedText}";
        private const string OriginalTextPlaceholder = "{OriginalText}";
        private const string TemplateKey = "template";

        private readonly IMessageGenerationService _messageGenerationService;
        private readonly IAssistantClient _assistantClient;
        private readonly string _assistantId;
        private readonly ITextComparisonService _textComparisonService;
        private readonly ITextExtractionService _textExtractionService;

        public TextCorrectionService(
            IMessageGenerationService messageGenerationService,
            IAssistantClient assistantClient,
            IOptionsMonitor<OpenAiConfig> openAiConfig,
            ITextComparisonService textComparisonService,
            ITextExtractionService textExtractionService)
        {
            _messageGenerationService = messageGenerationService;
            _assistantClient = assistantClient;
            _textComparisonService = textComparisonService;
            _textExtractionService = textExtractionService;
            _assistantId = openAiConfig.CurrentValue.EnglishTutorAssistantId!;
        }

        public async Task<(bool IsCorrected, string CorrectedText)> Correct(TextGenerationRequest request)
        {
            var message = await GenerateMessage(request);
            await _assistantClient.AddMessageToThread(
                new AddMessageToThreadModel(request.ThreadId, message, ChatType.TextCorrection));

            var runResponse = await _assistantClient.CreateRunRequest(_assistantId, request.ThreadId);

            if (runResponse.Status != RunStatus.Completed)
            {
                throw new InvalidOperationException("The text correction run did not complete successfully.");
            }

            var correctedText = await _assistantClient.GenerateLastMessage(
                new GenerateLastMessageModel(runResponse, request.ThreadId, ChatType.TextCorrection));

            var cleanCorrectedText = _textExtractionService.ExtractCleanText(correctedText, request.TranslatedText);
            var isCorrected = _textComparisonService.HasTextChanged(request.TranslatedText, cleanCorrectedText);

            return (isCorrected, correctedText);
        }

        private async Task<string> GenerateMessage(TextGenerationRequest request)
        {
            var message = await _messageGenerationService.Generate(
                new Dictionary<string, string>
                {
                    { TranslatedTextPlaceholder, request.TranslatedText },
                    { OriginalTextPlaceholder, request.OriginalText }
                },
                TemplateKey);

            return message;
        }
    }
}