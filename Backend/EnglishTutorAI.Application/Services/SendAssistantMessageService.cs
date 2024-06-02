using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Enums;
using Microsoft.Extensions.Options;
using OpenAI.Threads;

namespace EnglishTutorAI.Application.Services;

public class SendAssistantMessageService : ISendAssistantMessageService
{
    private readonly IAssistantClient _assistantClient;
    private readonly string _assistantId;
    private const string ErrorMessage = "Something went wrong...";

    public SendAssistantMessageService(IAssistantClient assistantClient, IOptionsMonitor<OpenAiConfig> openAiConfig)
    {
        _assistantClient = assistantClient;
        _assistantId = openAiConfig.CurrentValue.EnglishTutorAssistantId!;
    }

    public async Task<string> SendMessageAndRun(SendMessageRequest request)
    {
        await _assistantClient.AddMessageToThread(
            new AddMessageToThreadModel(request.ThreadId, request.Message, ChatType.Dialog));

        var runResponse = await _assistantClient.CreateRunRequest(_assistantId,  request.ThreadId);

        if (runResponse.Status == RunStatus.Completed)
        {
            return await _assistantClient.GenerateLastMessage(
                new GenerateLastMessageModel(runResponse, request.ThreadId, ChatType.Dialog));
        }

        return ErrorMessage;
    }
}