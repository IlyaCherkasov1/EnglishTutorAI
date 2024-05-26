using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using Microsoft.Extensions.Options;

namespace EnglishTutorAI.Application.Services;

public class AssistanceCreationService : IAssistanceCreationService
{
    private readonly IAssistantClient _assistantClient;
    private readonly string _assistantId;

    public AssistanceCreationService(IOptionsMonitor<OpenAiConfig> openAiConfig, IAssistantClient assistantClient)
    {
        _assistantClient = assistantClient;
        _assistantId = openAiConfig.CurrentValue.EnglishTutorAssistantId!;
    }

    public async Task<CreateAssistantResponse> Create()
    {
        var assistant = await _assistantClient.RetrieveAssistant(_assistantId);
        var thread = await _assistantClient.CreateThread();

        return new CreateAssistantResponse
        {
            AssistantId = assistant.Id,
            ThreadId = thread.Id,
        };
    }
}