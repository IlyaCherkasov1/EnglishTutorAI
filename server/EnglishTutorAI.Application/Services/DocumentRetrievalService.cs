using AutoMapper;
using EnglishTutorAI.Application.Attributes;
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

    public DocumentRetrievalService(
        IRepository<Document> documentRepository,
        IMapper mapper)
    {
        _documentRepository = documentRepository;
        _mapper = mapper;
    }

    public async Task<DocumentResponse> GetDocumentById(Guid id)
    {
        var document = await _documentRepository.GetSingleOrDefault(new DocumentRetrievalByIdSpecification(id));
        var documentResponse = _mapper.Map<DocumentResponse>(document);
        documentResponse.Sentences = document!.Sentences.OrderBy(s => s.Position)
            .Select(s => s.Text);

        return documentResponse;
    }
}