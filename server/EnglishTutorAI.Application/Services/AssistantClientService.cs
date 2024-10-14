using System.ClientModel;
using System.ClientModel.Primitives;
using AutoMapper;
using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Application.Utils;
using EnglishTutorAI.Domain.Enums;
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

    public AssistantClientService(
        IOptionsMonitor<OpenAiConfig> openAiConfig,
        IHttpClientFactory httpClientFactory,
        IRepository<ChatMessage> chatMessageRepository)
    {
        _chatMessageRepository = chatMessageRepository;
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
        var run = await _assistantClient.CreateRunAsync(threadId, assistantId);

        do
        {
            Thread.Sleep(TimeSpan.FromSeconds(1));
            run = await _assistantClient.GetRunAsync(threadId, run.Value.Id);
        } while (!run.Value.Status.IsTerminal);
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