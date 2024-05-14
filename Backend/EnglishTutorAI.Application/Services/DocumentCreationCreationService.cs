using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

public class DocumentCreationCreationService : IDocumentCreationService
{
    private readonly IRepository<Document> _documentRepository;

    public DocumentCreationCreationService(IRepository<Document> documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task AddDocument(DocumentCreationRequest creationRequest)
    {
        var document = new Document()
        {
            Title = creationRequest.Title,
            Content = creationRequest.Content,
        };

        await _documentRepository.Add(document);
    }
}