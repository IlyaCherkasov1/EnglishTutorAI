using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;
using Microsoft.Extensions.Options;

namespace EnglishTutorAI.Application.Services;

public class DocumentCreationCreationService : IDocumentCreationService
{
    private readonly IRepository<Document> _documentRepository;
    private readonly IAssistantClient _assistantClient;
    private readonly string _assistantId;

    public DocumentCreationCreationService(
        IRepository<Document> documentRepository,
        IAssistantClient assistantClient,
        IOptionsMonitor<OpenAiConfig> openAiConfig)
    {
        _documentRepository = documentRepository;
        _assistantClient = assistantClient;
        _assistantId = openAiConfig.CurrentValue.EnglishTutorAssistantId!;
    }

    public async Task AddDocument(DocumentCreationRequest creationRequest)
    {
        var document = new Document()
        {
            Title = creationRequest.Title,
            Content = creationRequest.Content,
            ThreadId = (await _assistantClient.CreateThread()).Id
        };

        await _documentRepository.Add(document);
    }
}