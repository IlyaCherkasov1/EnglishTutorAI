using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;
using Microsoft.Extensions.Options;

namespace EnglishTutorAI.Application.Services;

public class ThreadCreationService : IThreadCreationService
{
    private readonly IAssistantClient _assistantClient;
    private readonly IRepository<Document> _documentRepository;
    private readonly string _assistantId;

    public ThreadCreationService(
        IOptionsMonitor<OpenAiConfig> openAiConfig,
        IAssistantClient assistantClient,
        IRepository<Document> documentRepository)
    {
        _assistantClient = assistantClient;
        _documentRepository = documentRepository;
        _assistantId = openAiConfig.CurrentValue.EnglishTutorAssistantId!;
    }

    public async Task<ThreadCreationResponse> Create(Guid documentId)
    {
        var document = await _documentRepository.GetById(documentId);

        if (document.ThreadId  == null)
        {
            var threadId = (await _assistantClient.CreateThread()).Id;
            document.ThreadId = threadId;
        }

        return new ThreadCreationResponse
        {
            AssistantId = _assistantId,
            ThreadId = document.ThreadId,
        };
    }
}