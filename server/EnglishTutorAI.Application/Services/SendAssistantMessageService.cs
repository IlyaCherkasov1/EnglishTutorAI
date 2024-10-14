using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Enums;
using Microsoft.Extensions.Options;
using OpenAI.Assistants;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class SendAssistantMessageService : ISendAssistantMessageService
{
    private readonly IAssistantClientService _assistantClientService;
    private readonly string _assistantId;
    private readonly IChatMessageAddService _chatMessageAddService;

    public SendAssistantMessageService(
        IAssistantClientService assistantClientService,
        IOptionsMonitor<OpenAiConfig> openAiConfig,
        IChatMessageAddService chatMessageAddService)
    {
        _assistantClientService = assistantClientService;
        _chatMessageAddService = chatMessageAddService;
        _assistantId = openAiConfig.CurrentValue.EnglishTutorAssistantId!;
    }

    public async Task<string> SendMessageAndRun(SendMessageRequest request)
    {
        await SendMessage(request);
        await _assistantClientService.CreateRunRequest(_assistantId, request.ThreadId);

        var response = await _assistantClientService.GetLastMessage(request.ThreadId);
        await _chatMessageAddService.Add(
            new AddChatMessageModel(request.ThreadId, response, ChatType.Dialog, ConversationRole.Assistant));

        return response;
    }

    private async Task SendMessage(SendMessageRequest request)
    {
        await _assistantClientService.AddMessageToThread(request.ThreadId, request.Message);
        await _chatMessageAddService.Add(
            new AddChatMessageModel(request.ThreadId, request.Message, ChatType.Dialog, ConversationRole.User));
    }
}