using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services.Sentences;

public class SentenceRetrieverService : ISentenceRetrieverService
{
    private readonly IRepository<Sentence> _sentenceRepository;

    public SentenceRetrieverService(IRepository<Sentence> sentenceRepository)
    {
        _sentenceRepository = sentenceRepository;
    }

    public Task<IReadOnlyList<Sentence>> Get()
    {
        return _sentenceRepository.ListAll();
    }
}