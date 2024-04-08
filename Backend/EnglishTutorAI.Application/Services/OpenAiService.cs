using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using Microsoft.Extensions.Options;
using OpenAI_API;
using OpenAI_API.Chat;

namespace EnglishTutorAI.Application.Services
{
    public class OpenAiService : IOpenAiService
    {
        private const string Placeholder = "{Text}";
        private const string TemplateKey = "template";

        private readonly OpenAiConfig _openAiConfig;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IPromptTemplateService _promptTemplateService;

        public OpenAiService(
            IOptionsMonitor<OpenAiConfig> openAiConfig,
            IHttpClientFactory httpClientFactory,
            IPromptTemplateService promptTemplateService)
        {
            _openAiConfig = openAiConfig.CurrentValue;
            _httpClientFactory = httpClientFactory;
            _promptTemplateService = promptTemplateService;
        }

        public async Task<string> GenerateChatCompletion(string text)
        {
            var api = new OpenAIAPI(_openAiConfig.Key) { HttpClientFactory = _httpClientFactory };
            var prompt = await _promptTemplateService.GetFormattedPromptAsync(new PromptParameters
            {
                Placeholder = Placeholder,
                Text = text,
                TemplateKey = TemplateKey,
            });

            var chatRequest = new ChatRequest
            {
                Messages = new List<ChatMessage> { new(ChatMessageRole.User, prompt) },
            };

            var completionResult = await api.Chat.CreateChatCompletionAsync(chatRequest);

            return completionResult.ToString();
        }
    }
}