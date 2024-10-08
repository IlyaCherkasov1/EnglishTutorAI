using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;
using Microsoft.Extensions.Options;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class DocumentCreationCreationService : IDocumentCreationService
{
    private readonly IRepository<Document> _documentRepository;
    private readonly IAssistantClient _assistantClient;

    public DocumentCreationCreationService(IRepository<Document> documentRepository, IAssistantClient assistantClient)
    {
        _documentRepository = documentRepository;
        _assistantClient = assistantClient;
    }

    public async Task AddDocument(DocumentCreationRequest creationRequest)
    {
        var document = new Document()
        {
            Title = creationRequest.Title,
            Content = creationRequest.Content,
            StudyTopic = creationRequest.StudyTopic,
            ThreadId = (await _assistantClient.CreateThread()).Id,
        };

        await _documentRepository.Add(document);
    }
}