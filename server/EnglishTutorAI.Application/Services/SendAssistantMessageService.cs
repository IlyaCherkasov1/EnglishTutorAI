using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;
using EnglishTutorAI.Domain.Enums;
using Microsoft.Extensions.Options;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class SendAssistantMessageService : ISendAssistantMessageService
{
    private readonly IAssistantClientService _assistantClientService;
    private readonly string _assistantId;
    private readonly IRepository<DialogMessage> _dialogMessageRepository;

    public SendAssistantMessageService(
        IAssistantClientService assistantClientService,
        IOptionsMonitor<OpenAiConfig> openAiConfig,
        IRepository<DialogMessage> dialogMessageRepository)
    {
        _assistantClientService = assistantClientService;
        _dialogMessageRepository = dialogMessageRepository;
        _assistantId = openAiConfig.CurrentValue.EnglishTutorAssistantId!;
    }

    public async Task<string> SendMessageAndRun(SendMessageRequest request)
    {
        await SendMessage(request);
        await _assistantClientService.CreateRunRequest(_assistantId, request.ThreadId);
        var response = await _assistantClientService.GetLastMessage(request.ThreadId);
        await AddMessageToDialogRepository(request.ThreadId, response, ConversationRole.Assistant);

        return response;
    }

    private async Task SendMessage(SendMessageRequest request)
    {
        await _assistantClientService.AddMessageToThread(request.ThreadId, request.Message);
        await AddMessageToDialogRepository(request.ThreadId, request.Message, ConversationRole.User);
    }

    private async Task AddMessageToDialogRepository(string threadId, string content, ConversationRole role)
    {
        await _dialogMessageRepository.Add(new DialogMessage
        {
            ThreadId = threadId,
            Content = content,
            ConversationRole = role,
        });
    }
}