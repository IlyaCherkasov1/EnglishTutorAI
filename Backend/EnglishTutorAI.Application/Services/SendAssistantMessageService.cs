using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using OpenAI.Threads;

namespace EnglishTutorAI.Application.Services;

public class SendAssistantMessageService : ISendAssistantMessageService
{
    private readonly IAssistantClient _assistantClient;
    private const string ErrorMessage = "Something went wrong...";

    public SendAssistantMessageService(IAssistantClient assistantClient)
    {
        _assistantClient = assistantClient;
    }

    public async Task<string> SendMessageAndRun(SendMessageRequest request)
    {
        await _assistantClient.AddMessageToThread(request.ThreadId, request.Message);
        var runResponse = await _assistantClient.CreateRunRequest(request.AssistantId,  request.ThreadId);

        if (runResponse.Status == RunStatus.Completed)
        {
            return await _assistantClient.GetLastMessage(runResponse);
        }

        return ErrorMessage;
    }
}