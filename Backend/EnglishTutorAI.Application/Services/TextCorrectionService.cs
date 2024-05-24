    using EnglishTutorAI.Application.Configurations;
    using EnglishTutorAI.Application.Interfaces;
    using EnglishTutorAI.Application.Models.TextGeneration;
    using Microsoft.Extensions.Options;
    using OpenAI.Assistants;
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
            private AssistantResponse? _currentAssistant;
            private readonly string _assistantId;
            private string? _currentThreadId;

            public TextCorrectionService(
                IOptionsMonitor<OpenAiConfig> openAiConfig,
                IMessageGenerationService messageGenerationService,
                IAssistantClient assistantClient)
            {
                _messageGenerationService = messageGenerationService;
                _assistantClient = assistantClient;
                _assistantId = openAiConfig.CurrentValue.EnglishFixerAssistantId!;
            }

            public async Task<(bool IsCorrected, string CorrectedText)> Correct(TextGenerationRequest request)
            {
                _currentAssistant ??= await _assistantClient.RetrieveAssistant(_assistantId);

                if (string.IsNullOrEmpty(_currentThreadId))
                {
                    var thread = await _assistantClient.CreateThread();
                    _currentThreadId = thread.Id;
                }

                var message = await GenerateMessage(request);
                await _assistantClient.AddMessageToThread(_currentThreadId, message);
                var runResponse = await _assistantClient.CreateRunRequest(_currentAssistant.Id,  _currentThreadId);

                if (runResponse.Status != RunStatus.Completed)
                {
                    throw new InvalidOperationException("The text correction run did not complete successfully.");
                }

                var correctedText = await _assistantClient.GetLastMessage(runResponse);
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