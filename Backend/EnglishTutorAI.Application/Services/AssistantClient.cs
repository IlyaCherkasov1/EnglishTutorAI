using System.Text.Json;
using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;
using EnglishTutorAI.Domain.Enums;
using Microsoft.Extensions.Options;
using OpenAI;
using OpenAI.Assistants;
using OpenAI.Threads;
using Message = OpenAI.Threads.Message;

namespace EnglishTutorAI.Application.Services;

public class AssistantClient : IAssistantClient
{
    private readonly OpenAIClient _api;
    private readonly IRepository<ChatMessage> _chatMessageRepository;

    public AssistantClient(
        IOptionsMonitor<OpenAiConfig> openAiConfig,
        IHttpClientFactory httpClientFactory,
        IRepository<ChatMessage> chatMessageRepository)
    {
        _chatMessageRepository = chatMessageRepository;
        var customHttpClient = httpClientFactory.CreateClient();
        _api = new OpenAIClient(openAiConfig.CurrentValue.Key, client: customHttpClient);
    }

    public async Task<AssistantResponse> RetrieveAssistant(string assistantId)
    {
        var assistant = await _api.AssistantsEndpoint.RetrieveAssistantAsync(assistantId);

        return assistant;
    }

    public async Task<ThreadResponse> CreateThread()
    {
        var thread = await _api.ThreadsEndpoint.CreateThreadAsync();

        return thread;
    }

    public async Task AddMessageToThread(AddMessageToThreadModel addMessageToThreadModel)
    {
        var textMessage = new Message(addMessageToThreadModel.Content);
        await _api.ThreadsEndpoint.CreateMessageAsync(addMessageToThreadModel.ThreadId, textMessage);

        var chatMessage = new ChatMessage
        {
            Content = addMessageToThreadModel.Content,
            CreatedAt = DateTime.UtcNow,
            ThreadId = addMessageToThreadModel.ThreadId,
            ConversationRole = ConversationRole.User,
            ChatType = addMessageToThreadModel.ChatType,
        };

        await _chatMessageRepository.Add(chatMessage);
    }

    public async Task<RunResponse> CreateRunRequest(string assistantId, string threadId)
    {
        var createRunRequest = new CreateRunRequest(assistantId);
        var run = await _api.ThreadsEndpoint.CreateRunAsync(threadId, createRunRequest);

        return await run.WaitForStatusChangeAsync();
    }

    public async Task<string> GenerateLastMessage(GenerateLastMessageModel model)
    {
        var messagesResponse = await model.Run.ListMessagesAsync();
        var lastMessage = messagesResponse.Items.Last();
        List<MessageContentResponse> response = JsonSerializer.Deserialize<List<MessageContentResponse>>(lastMessage.Content);
        var assistantResponse = response.First().Text.Value;

        var chatMessage = new ChatMessage
        {
            Content = assistantResponse,
            CreatedAt = DateTime.UtcNow,
            ThreadId = model.ThreadId,
            ConversationRole = ConversationRole.Assistant,
            ChatType = model.ChatType,
        };

        await _chatMessageRepository.Add(chatMessage);

        return assistantResponse;
    }

    public async Task<IReadOnlyList<ChatMessage>> GetAllMessages(string threadId, ChatType chatType)
    {
        var messages = await _chatMessageRepository.List(new ChatMessagesByThreadIdSpecification(threadId, chatType));

        var userMessages = messages.Where(m => m.ConversationRole == ConversationRole.User).ToList();
        var botMessages = messages.Where(m => m.ConversationRole == ConversationRole.Assistant).ToList();

        var orderedMessages = userMessages
            .Select((m, i) => new { Message = m, Index = i })
            .SelectMany(x => new[] { x.Message, botMessages.ElementAtOrDefault(x.Index) })
            .Where(m => m != null)
            .ToList();

        return orderedMessages;
    }
}