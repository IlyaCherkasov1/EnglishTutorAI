using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;
using EnglishTutorAI.Domain.Enums;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using OpenAI.Threads;

namespace EnglishTutorAI.Application.Services;

public class SendAssistantMessageService : ISendAssistantMessageService
{
    private readonly IAssistantClient _assistantClient;
    private readonly string _assistantId;
    private readonly IMessageGenerationService _messageGenerationService;
    private readonly IChatMessageAddService _chatMessageAddService;
    private const string ErrorMessage = "Something went wrong...";
    private const string TemplateKey = "englishAssistantTemplate";
    private const string MessagePlaceholder = "Message";

    public SendAssistantMessageService(
        IAssistantClient assistantClient,
        IOptionsMonitor<OpenAiConfig> openAiConfig,
        IMessageGenerationService messageGenerationService,
        IChatMessageAddService chatMessageAddService)
    {
        _assistantClient = assistantClient;
        _messageGenerationService = messageGenerationService;
        _chatMessageAddService = chatMessageAddService;
        _assistantId = openAiConfig.CurrentValue.EnglishTutorAssistantId!;
    }

    public async Task<string> SendMessageAndRun(SendMessageRequest request)
    {
        await SendMessage(request);
        var runResponse = await _assistantClient.CreateRunRequest(_assistantId,  request.ThreadId);

        if (runResponse.Status == RunStatus.Completed)
        {
            var response = await _assistantClient.GenerateLastMessage(
                new GenerateLastMessageModel(runResponse, request.ThreadId, ChatType.Dialog));
            await _chatMessageAddService.Add(
                new AddChatMessageModel(request.ThreadId, response, ChatType.Dialog, ConversationRole.Assistant));

            return response;
        }

        return ErrorMessage;
    }

    private async Task SendMessage(SendMessageRequest request)
    {
        var placeholderValues = new Dictionary<string, string> {{ MessagePlaceholder, request.Message }};
        var message = await _messageGenerationService.Generate(placeholderValues, TemplateKey);
        await _assistantClient.AddMessageToThread(request.ThreadId, message);
        await _chatMessageAddService.Add(
            new AddChatMessageModel(request.ThreadId, request.Message, ChatType.Dialog, ConversationRole.User));
    }
}