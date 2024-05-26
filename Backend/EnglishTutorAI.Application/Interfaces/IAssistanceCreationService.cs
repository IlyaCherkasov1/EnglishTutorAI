using EnglishTutorAI.Application.Models;

namespace EnglishTutorAI.Application.Interfaces;

public interface IAssistanceCreationService
{
    Task<CreateAssistantResponse> Create();
}