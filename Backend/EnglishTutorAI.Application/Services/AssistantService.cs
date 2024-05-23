using EnglishTutorAI.Application.Interfaces;
using OpenAI.Assistants;
using OpenAI.Threads;

namespace EnglishTutorAI.Application.Services;

public class AssistantService : IAssistantService
{
    private readonly IAssistantClient _assistantClient;
    private string? _currentThreadId;
    private AssistantResponse? _currentAssistant;
    private const string ErrorMessage = "Something went wrong...";

    public AssistantService(IAssistantClient assistantClient)
    {
        _assistantClient = assistantClient;
    }

    public async Task<string> StartConversation(string message)
    {
        _currentAssistant ??= await _assistantClient.RetrieveAssistant();

        if (string.IsNullOrEmpty(_currentThreadId))
        {
            var thread = await _assistantClient.CreateThread();
            _currentThreadId = thread.Id;
        }

        await _assistantClient.AddMessageToThread(_currentThreadId, message);
        var runResponse = await _assistantClient.CreateRunRequest(_currentAssistant.Id,  _currentThreadId);

        if (runResponse.Status == RunStatus.Completed)
        {
            return await _assistantClient.GetLastMessage(runResponse);
        }

        return ErrorMessage;
    }
}