using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models.TextGeneration;
using Microsoft.Extensions.Options;
using OpenAI;
using OpenAI.Chat;
using OpenAI.Models;

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
            using var customHttpClient = _httpClientFactory.CreateClient();
            var api = new OpenAIClient(_openAiConfig.Key, client: customHttpClient);

            var chatRequest = await GenerateChatRequest(request);

            var correctedText = (await api.ChatEndpoint.GetCompletionAsync(chatRequest)).ToString();
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

            var messages = new List<Message> { new(Role.User, prompt) };
            var chatRequest = new ChatRequest(messages, Model.GPT3_5_Turbo);

            return chatRequest;
        }
    }
}