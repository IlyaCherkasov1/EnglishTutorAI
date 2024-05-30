    using EnglishTutorAI.Application.Configurations;
    using EnglishTutorAI.Application.Interfaces;
    using EnglishTutorAI.Application.Models;
    using EnglishTutorAI.Application.Models.TextGeneration;
    using EnglishTutorAI.Domain.Enums;
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

            public TextCorrectionService(
                IMessageGenerationService messageGenerationService,
                IAssistantClient assistantClient)
            {
                _messageGenerationService = messageGenerationService;
                _assistantClient = assistantClient;
            }

            public async Task<(bool IsCorrected, string CorrectedText)> Correct(TextGenerationRequest request)
            {
                var message = await GenerateMessage(request);
                await _assistantClient.AddMessageToThread(
                    new AddMessageToThreadModel(request.ThreadId, message, ChatType.TextCorrection));

                var runResponse = await _assistantClient.CreateRunRequest(request.AssistantId,  request.ThreadId);

                if (runResponse.Status != RunStatus.Completed)
                {
                    throw new InvalidOperationException("The text correction run did not complete successfully.");
                }

                var correctedText = await _assistantClient.GenerateLastMessage(
                    new GenerateLastMessageModel(runResponse, request.ThreadId, ChatType.TextCorrection));
                var isCorrected = !correctedText.Equals(request.TranslatedText, StringComparison.OrdinalIgnoreCase);

                return (isCorrected, correctedText);
            }

            private async Task<string> GenerateMessage(TextGenerationRequest request)
            {
                if (string.IsNullOrEmpty(request.OriginalText) || string.IsNullOrEmpty(request.TranslatedText))
                {
                    throw new ArgumentException("Text cannot be null or empty.");
                }

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