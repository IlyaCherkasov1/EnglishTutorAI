using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Interfaces;
using Microsoft.Extensions.Options;
using OpenAI_API;
using OpenAI_API.Chat;

namespace EnglishTutorAI.Application.Services
{
    public class OpenAiService : IOpenAiService
    {
        private readonly OpenAiConfig _openAiConfig;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IPromptTemplateService _promptTemplateService;
        private readonly IElevenLabsService _elevenLabsService;

        public OpenAiService(
            IOptionsMonitor<OpenAiConfig> openAiConfig,
            IHttpClientFactory httpClientFactory,
            IPromptTemplateService promptTemplateService,
            IElevenLabsService elevenLabsService)
        {
            _openAiConfig = openAiConfig.CurrentValue;
            _httpClientFactory = httpClientFactory;
            _promptTemplateService = promptTemplateService;
            _elevenLabsService = elevenLabsService;
        }

        public async Task GenerateSentences(string phrase)
        {
            var api = new OpenAIAPI(_openAiConfig.Key) { HttpClientFactory = _httpClientFactory };
            var prompt = await _promptTemplateService.GetFormattedPromptAsync(phrase);

            var chatRequest = new ChatRequest
            {
                Messages = new List<ChatMessage> { new(ChatMessageRole.User, prompt) },
            };

            var generatedText = (await api.Chat.CreateChatCompletionAsync(chatRequest)).ToString();
            await _elevenLabsService.GenerateSpeechAsync(generatedText);
        }
    }
}