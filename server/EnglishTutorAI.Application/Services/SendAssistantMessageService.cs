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
        IOptions<OpenAiConfig> openAiConfig,
        IRepository<DialogMessage> dialogMessageRepository)
    {
        _assistantClientService = assistantClientService;
        _dialogMessageRepository = dialogMessageRepository;
        _assistantId = openAiConfig.Value.EnglishTutorAssistantId!;
    }

    public Task<string> SendMessage(SendMessageRequest request) =>
        SendMessageInternal(request);

    public Task<string> SendMessageAndSaveToTheRepository(SendMessageRequest request) =>
        SendMessageInternal(request, saveToRepository: true);

    private async Task<string> SendMessageInternal(SendMessageRequest request, bool saveToRepository = false)
    {
        await _assistantClientService.AddMessageToThread(request.ThreadId, request.Message);
        await _assistantClientService.CreateRunRequestWithStreaming(_assistantId, request.ThreadId, request.GroupId);
        var lastMessage = await _assistantClientService.GetLastMessage(request.ThreadId);

        if (saveToRepository)
        {
            await AddMessageToDialogRepository(new AddMessageToDialogRepositoryModel(
                request.UserTranslateId, request.Message, ConversationRole.User));

            await AddMessageToDialogRepository(new AddMessageToDialogRepositoryModel(
                request.UserTranslateId, lastMessage, ConversationRole.Assistant));
        }

        return lastMessage;
    }

    private async Task AddMessageToDialogRepository(AddMessageToDialogRepositoryModel model)
    {
        await _dialogMessageRepository.Add(new DialogMessage
        {
            Content = model.Content,
            ConversationRole = model.Role,
            UserTranslateId = model.UserTranslateId
        });
    }
}