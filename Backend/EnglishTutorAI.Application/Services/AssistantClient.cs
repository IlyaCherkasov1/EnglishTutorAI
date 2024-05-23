using System.Text.Json;
using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using Microsoft.Extensions.Options;
using OpenAI;
using OpenAI.Assistants;
using OpenAI.Threads;
using Message = OpenAI.Threads.Message;

namespace EnglishTutorAI.Application.Services;

public class AssistantClient : IAssistantClient
{
    private readonly OpenAIClient _api;
    private readonly string _assistantId;

    public AssistantClient(IOptionsMonitor<OpenAiConfig> openAiConfig, IHttpClientFactory httpClientFactory)
    {
        var customHttpClient = httpClientFactory.CreateClient();
        _api = new OpenAIClient(openAiConfig.CurrentValue.Key, client: customHttpClient);
        _assistantId = openAiConfig.CurrentValue.EnglishTutorAssistantId!;
    }

    public async Task<AssistantResponse> RetrieveAssistant()
    {
        var assistant = await _api.AssistantsEndpoint.RetrieveAssistantAsync(_assistantId);

        return assistant;
    }

    public async Task<ThreadResponse> CreateThread()
    {
        var thread = await _api.ThreadsEndpoint.CreateThreadAsync();

        return thread;
    }

    public async Task AddMessageToThread(string threadId, string content)
    {
        var textMessage = new Message(content);
        await _api.ThreadsEndpoint.CreateMessageAsync(threadId, textMessage);
    }

    public async Task<RunResponse> CreateRunRequest(string assistantId, string threadId)
    {
        var createRunRequest = new CreateRunRequest(assistantId);
        var run = await _api.ThreadsEndpoint.CreateRunAsync(threadId, createRunRequest);
        return await run.WaitForStatusChangeAsync();
    }

    public async Task<string> GetLastMessage(RunResponse run)
    {
        var messagesResponse = await run.ListMessagesAsync();
        var lastMessage = messagesResponse.Items.Last();
        List<MessageContentResponse> response = JsonSerializer.Deserialize<List<MessageContentResponse>>(lastMessage.Content);

        return response.First().Text.Value;
    }

    public async Task<List<MessageHistoryItem>> GetAllMessages(RunResponse run)
    {
        var messageHistory = new List<MessageHistoryItem>();
        var messages = await run.ListMessagesAsync();

        foreach (var message in messages.Items.OrderBy(response => response.CreatedAt))
        {
            List<MessageContentResponse> response = JsonSerializer.Deserialize<List<MessageContentResponse>>(message.Content);
            messageHistory.Add(new MessageHistoryItem
            {
                Role = message.Role.ToString(),
                Content = response.First().Text.Value
            });
        }

        return messageHistory;
    }
}