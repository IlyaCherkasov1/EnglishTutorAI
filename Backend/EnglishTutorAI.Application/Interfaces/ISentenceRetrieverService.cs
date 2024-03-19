using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Interfaces;

public interface ISentenceRetrieverService
{
    Task<IReadOnlyList<Sentence>>  Get();
}