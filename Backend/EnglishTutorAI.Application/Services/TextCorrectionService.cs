using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.TextGeneration;
using EnglishTutorAI.Domain.Extensions;
using Microsoft.Extensions.Options;
using OpenAI_API;
using OpenAI_API.Chat;

namespace EnglishTutorAI.Application.Services
{
    public class TextCorrectionService : ITextCorrectionService
    {
        private const string TranslatedTextPlaceholder = "{TranslatedText}";
        private const string OriginalTextPlaceholder = "{OriginalText}";
        private const string TemplateKey = "template";

        private readonly OpenAiConfig _openAiConfig;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IPromptTemplateService _promptTemplateService;

        public TextCorrectionService(
            IOptionsMonitor<OpenAiConfig> openAiConfig,
            IHttpClientFactory httpClientFactory,
            IPromptTemplateService promptTemplateService)
        {
            _openAiConfig = openAiConfig.CurrentValue;
            _httpClientFactory = httpClientFactory;
            _promptTemplateService = promptTemplateService;
        }

        public async Task<(bool IsCorrected, string CorrectedText)> Correct(
            TextGenerationRequest request)
        {
            var api = new OpenAIAPI(_openAiConfig.Key) { HttpClientFactory = _httpClientFactory };
            var chatRequest = await GenerateChatRequest(request);

            var correctedText = (await api.Chat.CreateChatCompletionAsync(chatRequest)).ToString();
            var isCorrected = !correctedText.Equals(request.TranslatedText, StringComparison.OrdinalIgnoreCase);

            return (isCorrected, correctedText);
        }

        private async Task<ChatRequest> GenerateChatRequest(TextGenerationRequest request)
        {
            if (string.IsNullOrEmpty(request.OriginalText) || string.IsNullOrEmpty(request.TranslatedText))
            {
                throw new ArgumentException("Text cannot be null or empty.");
            }

            var prompt = await _promptTemplateService.GetFormattedPromptAsync(
                new Dictionary<string, string>
                {
                    { TranslatedTextPlaceholder, request.TranslatedText },
                    { OriginalTextPlaceholder, request.OriginalText }
                },
                TemplateKey);

            var chatRequest = new ChatRequest
            {
                Messages = new List<ChatMessage> { new(ChatMessageRole.User, prompt) },
            };

            return chatRequest;
        }
    }
}