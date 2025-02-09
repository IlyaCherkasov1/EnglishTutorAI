﻿using System.ClientModel;
using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Hubs;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;
using EnglishTutorAI.Domain.Enums;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using OpenAI.Assistants;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class AssistantClientService : IAssistantClientService
{
    private readonly IRepository<DialogMessage> _dialogMessageRepository;
    private readonly AssistantClient _assistantClient;
    private readonly IHubContext<AssistantHub> _assistantHubContext;

    public AssistantClientService(
        IOptions<OpenAiConfig> openAiConfig,
        IHubContext<AssistantHub> assistantHubContext,
        IRepository<DialogMessage> dialogMessageRepository)
    {
        _assistantHubContext = assistantHubContext;
        _dialogMessageRepository = dialogMessageRepository;
        _assistantClient = new AssistantClient(new ApiKeyCredential(openAiConfig.Value.Key!));
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

    public async Task CreateRunRequestWithStreaming(string assistantId, string threadId, string groupId)
    {
        var responseStream = _assistantClient.CreateRunStreamingAsync(threadId, assistantId);

        await foreach (var streamedMessage in responseStream)
        {
            if (streamedMessage is MessageContentUpdate contentUpdate)
            {
                await _assistantHubContext.Clients.Group(groupId).SendAsync("ReceiveMessage", contentUpdate.Text);
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

    public async Task<IEnumerable<DialogMessage>> GetAllMessages(string threadId)
    {
        var userMessages = await _dialogMessageRepository.List(new DialogMessagesByThreadIdSpecification(
            threadId, ConversationRole.User));

        var assistantMessages = await _dialogMessageRepository.List(new DialogMessagesByThreadIdSpecification(
            threadId, ConversationRole.Assistant));

        if (userMessages.Count != assistantMessages.Count)
        {
            throw new InvalidOperationException(
                "The number of user messages does not match the number of assistant messages.");
        }

        var orderedMessages = userMessages
            .Zip(assistantMessages, (userMsg, assistantMsg) => new[] { userMsg, assistantMsg })
            .SelectMany(x => x);

        return orderedMessages;
    }
}