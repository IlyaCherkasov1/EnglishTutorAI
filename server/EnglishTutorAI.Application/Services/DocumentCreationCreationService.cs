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
    private readonly IAssistantClientService _assistantClientService;
    private readonly ISentenceSplitterService _sentenceSplitterService;
    private readonly IDocumentSessionService _documentSessionService;

    public DocumentCreationCreationService(
        IRepository<Document> documentRepository,
        IAssistantClientService assistantClientService,
        ISentenceSplitterService sentenceSplitterService,
        IDocumentSessionService documentSessionService)
    {
        _documentRepository = documentRepository;
        _assistantClientService = assistantClientService;
        _sentenceSplitterService = sentenceSplitterService;
        _documentSessionService = documentSessionService;
    }

    public async Task AddDocument(DocumentCreationRequest creationRequest)
    {
        var document = new Document()
        {
            Title = creationRequest.Title,
            StudyTopic = creationRequest.StudyTopic,
            ThreadId = (await _assistantClientService.CreateThread()).Id,
        };

        document.Sentences = _sentenceSplitterService.Split(creationRequest.Content)
            .Select((text, index) => new DocumentSentence
            {
                DocumentId = document.Id,
                Text = text,
                Position = index + 1
            }).ToList();

        await _documentRepository.Add(document);
        await _documentSessionService.StartNewSession(document.Id);
    }
}