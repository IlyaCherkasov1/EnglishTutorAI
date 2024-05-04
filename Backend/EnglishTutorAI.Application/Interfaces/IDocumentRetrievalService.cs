﻿using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Interfaces;

public interface IDocumentRetrievalService
{
    Task<Document> GetDocumentByIndex(int index);

    Task<Document> GetDocumentById(Guid id);

    Task<IReadOnlyList<Document>> GetAllDocuments();
}