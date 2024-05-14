using AutoMapper;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

public class DocumentRetrievalService : IDocumentRetrievalService
{
    private readonly IRepository<Document> _documentRepository;

    public DocumentRetrievalService(IRepository<Document> documentRepository)
    {
        _documentRepository = documentRepository;
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

    public Task<Document> GetDocumentById(Guid id)
    {
        return _documentRepository.GetById(id);
    }

    public Task<IReadOnlyList<Document>> GetAllDocuments()
    {
        return _documentRepository.ListAll();
    }
}