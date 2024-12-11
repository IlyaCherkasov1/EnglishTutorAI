using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models.Translates;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class TranslateMistakeHistoryService : ITranslateMistakeHistoryService
{
    private readonly IRepository<LinguaFixMessage> _linguaFixMessageRepository;
    private readonly IRepository<UserTranslate> _userTranslateRepository;

    public TranslateMistakeHistoryService(
        IRepository<LinguaFixMessage> linguaFixMessageRepository,
        IRepository<UserTranslate> userTranslateRepository)
    {
        _linguaFixMessageRepository = linguaFixMessageRepository;
        _userTranslateRepository = userTranslateRepository;
    }

    public async Task<IEnumerable<TranslateMistakeHistoryItems>> Get(Guid userTranslateId)
    {
        var sessionId = await _userTranslateRepository.Single(new SessionIdByUserTranslateSpecification(userTranslateId));

        return await _linguaFixMessageRepository.List(new TranslateMistakeHistoryItemsSpecification(sessionId));
    }
}