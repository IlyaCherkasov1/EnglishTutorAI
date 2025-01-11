using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class StatisticsService : IStatisticsService
{
    private readonly IRepository<LinguaFixMessage> _linguaFixMessageRepository;
    private readonly IRepository<UserTranslate> _userTranslateRepository;
    private readonly IUserStatisticsService _userStatisticsService;

    public StatisticsService(
        IRepository<LinguaFixMessage> linguaFixMessageRepository,
        IRepository<UserTranslate> userTranslateRepository,
        IUserStatisticsService userStatisticsService)
    {
        _linguaFixMessageRepository = linguaFixMessageRepository;
        _userTranslateRepository = userTranslateRepository;
        _userStatisticsService = userStatisticsService;
    }


    public async Task SaveStatisticsAndMessage(SaveStatisticsAndMessageModel model)
    {
        await _userStatisticsService.UpdateStatisticsAndSaveMessage(model.TranslatedText, model.CorrectedText);
        var sessionId = await _userTranslateRepository.Single(new SessionIdByUserTranslateSpecification(model.UserTranslateId));

        await _linguaFixMessageRepository.Add(new LinguaFixMessage
        {
            TranslatedText = model.TranslatedText,
            CorrectedText = model.CorrectedText,
            UserTranslateId = model.UserTranslateId,
            SessionId = sessionId,
        });
    }
}