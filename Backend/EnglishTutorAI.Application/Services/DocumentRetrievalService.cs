using AutoMapper;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

public class DocumentRetrievalService : IDocumentRetrievalService
{
    private readonly IRepository<Document> _documentRepository;
    private readonly IMapper _mapper;

    public DocumentRetrievalService(IRepository<Document> documentRepository, IMapper mapper)
    {
        _documentRepository = documentRepository;
        _mapper = mapper;
    }

    public async Task<Document> GetDocumentByIndex(int index)
    {
        var documentCount = await _documentRepository.Count();

        if (index >= 0 && index < documentCount)
        {
            return (await _documentRepository.GetByIndex(index, new DocumentByIndexSpecification()))!;
        }

        throw new IndexOutOfRangeException("Index is out of range.");
    }

    public async Task<IReadOnlyList<DocumentListItem>> GetAllDocuments()
    {
        return _mapper.Map<IReadOnlyList<DocumentListItem>>(await _documentRepository.ListAll());
    }
}