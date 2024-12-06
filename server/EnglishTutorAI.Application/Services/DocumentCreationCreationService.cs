using Amazon.Runtime.Documents;
using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;
using Microsoft.Extensions.Options;
using Document = EnglishTutorAI.Domain.Entities.Document;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class DocumentCreationCreationService : IDocumentCreationService
{
    private readonly IRepository<Document> _documentRepository;
    private readonly ISentenceSplitterService _sentenceSplitterService;

    public DocumentCreationCreationService(
        IRepository<Document> documentRepository,
        ISentenceSplitterService sentenceSplitterService)
    {
        _documentRepository = documentRepository;
        _sentenceSplitterService = sentenceSplitterService;
    }

    public async Task AddDocument(DocumentCreationRequest creationRequest)
    {
        var document = new Document()
        {
            Title = creationRequest.Title,
            StudyTopic = creationRequest.StudyTopic,
        };

        document.Sentences = _sentenceSplitterService.Split(creationRequest.Content)
            .Select((text, index) => new DocumentSentence
            {
                DocumentId = document.Id,
                Text = text,
                Position = index + 1
            }).ToList();

        await _documentRepository.Add(document);
    }
}