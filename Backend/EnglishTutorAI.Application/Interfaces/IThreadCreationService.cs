using EnglishTutorAI.Application.Models;

namespace EnglishTutorAI.Application.Interfaces;

public interface IThreadCreationService
{
    Task<ThreadCreationResponse> Create(Guid documentId);
}