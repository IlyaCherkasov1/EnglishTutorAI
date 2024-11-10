using AutoMapper;
using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Exceptions;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class DocumentRetrievalService : IDocumentRetrievalService
{
    private readonly IRepository<Document> _documentRepository;
    private readonly IMapper _mapper;
    private readonly IRepository<DocumentSession> _documentSessionRepository;

    public DocumentRetrievalService(
        IRepository<Document> documentRepository,
        IMapper mapper,
        IRepository<DocumentSession> documentSessionRepository)
    {
        _documentRepository = documentRepository;
        _mapper = mapper;
        _documentSessionRepository = documentSessionRepository;
    }

    public async Task<DocumentResponse> GetDocumentById(Guid id)
    {
        var document = await _documentRepository.GetSingleOrDefault(new DocumentRetrievalByIdSpecification(id));

        if (document == null)
        {
            throw new EntityNotFoundException("Document not found");
        }

        var documentResponse = _mapper.Map<DocumentResponse>(document);
        documentResponse.SessionId =
            await _documentSessionRepository.GetSingleOrDefault(new DocumentSessionByDocumentIdSpecification(document.Id));

        return documentResponse;
    }
}