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
    private readonly IMessageGenerationService _messageGenerationService;
    private readonly IChatMessageAddService _chatMessageAddService;
    private const string TemplateKey = "englishAssistantTemplate";
    private const string MessagePlaceholder = "{Message}";

    public SendAssistantMessageService(
        IAssistantClientService assistantClientService,
        IOptionsMonitor<OpenAiConfig> openAiConfig,
        IMessageGenerationService messageGenerationService,
        IChatMessageAddService chatMessageAddService)
    {
        _assistantClientService = assistantClientService;
        _messageGenerationService = messageGenerationService;
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
        var placeholderValues = new Dictionary<string, string> { { MessagePlaceholder, request.Message } };
        var message = await _messageGenerationService.Generate(placeholderValues, TemplateKey);
        await _assistantClientService.AddMessageToThread(request.ThreadId, message);
        await _chatMessageAddService.Add(
            new AddChatMessageModel(request.ThreadId, request.Message, ChatType.Dialog, ConversationRole.User));
    }
}