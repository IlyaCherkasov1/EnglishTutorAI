using System.ClientModel;
using System.ClientModel.Primitives;
using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Hubs;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Enums;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using OpenAI;
using OpenAI.Assistants;
using ChatMessage = EnglishTutorAI.Domain.Entities.ChatMessage;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class AssistantClientService : IAssistantClientService
{
    private readonly IRepository<ChatMessage> _chatMessageRepository;
    private readonly AssistantClient _assistantClient;
    private readonly IHubContext<AssistantHub> _assistantHubContext;

    public AssistantClientService(
        IOptionsMonitor<OpenAiConfig> openAiConfig,
        IHttpClientFactory httpClientFactory,
        IRepository<ChatMessage> chatMessageRepository,
        IHubContext<AssistantHub> assistantHubContext)
    {
        _chatMessageRepository = chatMessageRepository;
        _assistantHubContext = assistantHubContext;
        var customHttpClient = httpClientFactory.CreateClient();
        var options = new OpenAIClientOptions
        {
            Transport = new HttpClientPipelineTransport(customHttpClient)
        };

        _assistantClient = new AssistantClient(new ApiKeyCredential(openAiConfig.CurrentValue.Key!), options);
    }

    public async Task<AssistantThread> CreateThread()
    {
        var clientResult = await _assistantClient.CreateThreadAsync();

        return clientResult.Value;
    }

    public async Task AddMessageToThread(string threadId, string content)
    {
        await _assistantClient.CreateMessageAsync(threadId, MessageRole.User, [content]);
    }

    public async Task CreateRunRequest(string assistantId, string threadId)
    {
        var responseStream = _assistantClient.CreateRunStreamingAsync(threadId, assistantId);

        await foreach (var streamedMessage in responseStream)
        {
            if (streamedMessage is MessageContentUpdate contentUpdate)
            {
                await _assistantHubContext.Clients.Group(threadId).SendAsync("ReceiveMessage", contentUpdate.Text);
            }
        }
    }

    public async Task<string> GetLastMessage(string threadId)
    {
        var messages = _assistantClient.GetMessagesAsync(threadId);

        await foreach (var message in messages)
        {
            if (message.Content.Count > 0)
            {
                return message.Content[0].Text;
            }
        }

        throw new InvalidOperationException("No messages available in the thread.");
    }

    public async Task<IEnumerable<ChatMessage>> GetAllMessages(string threadId)
    {
        var userMessages = await _chatMessageRepository.List(new ChatMessagesByThreadIdSpecification(
            threadId, ConversationRole.User));

        var assistantMessages = await _chatMessageRepository.List(new ChatMessagesByThreadIdSpecification(
            threadId, ConversationRole.Assistant));

        if (userMessages.Count != assistantMessages.Count)
        {
            throw new InvalidOperationException("The number of user messages does not match the number of assistant messages.");
        }

        var orderedMessages = userMessages
            .Zip(assistantMessages, (userMsg, assistantMsg) => new[] { userMsg, assistantMsg })
            .SelectMany(x => x);

        return orderedMessages;
    }
}