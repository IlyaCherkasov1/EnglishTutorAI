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
    private readonly IAssistantClientService _assistantClientService;

    public DocumentCreationCreationService(IRepository<Document> documentRepository, IAssistantClientService assistantClientService)
    {
        _documentRepository = documentRepository;
        _assistantClientService = assistantClientService;
    }

    public async Task AddDocument(DocumentCreationRequest creationRequest)
    {
        var document = new Document()
        {
            Title = creationRequest.Title,
            Content = creationRequest.Content,
            StudyTopic = creationRequest.StudyTopic,
            ThreadId = (await _assistantClientService.CreateThread()).Id,
        };

        await _documentRepository.Add(document);
    }
}