﻿using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class DocumentRetrievalService : IDocumentRetrievalService
{
    private readonly IRepository<Document> _documentRepository;

    public DocumentRetrievalService(IRepository<Document> documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public Task<Document> GetDocumentById(Guid id)
    {
        return _documentRepository.GetById(id);
    }
}