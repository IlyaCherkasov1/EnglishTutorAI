using EnglishTutorAI.Application.Models.Responses;

namespace EnglishTutorAI.Application.Interfaces;

public interface IContextService
{
    Task<ContextResponse> Load();
}