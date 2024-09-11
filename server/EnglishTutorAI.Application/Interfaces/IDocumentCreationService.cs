using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Interfaces;

public interface IDocumentCreationService
{
    Task AddDocument(DocumentCreationRequest creationRequest);
}