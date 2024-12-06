using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class UserStatisticsService : IUserStatisticsService
{
    private readonly ITextProcessingService _textProcessingService;
    private readonly IUserContextService _userContextService;
    private readonly IRepository<UserStatistics> _userStatisticsRepository;

    public UserStatisticsService(
        ITextProcessingService textProcessingService,
        IUserContextService userContextService,
        IRepository<UserStatistics> userStatisticsRepository)
    {
        _textProcessingService = textProcessingService;
        _userContextService = userContextService;
        _userStatisticsRepository = userStatisticsRepository;
    }

    public async Task UpdateStatisticsAndSaveMessage(
        string translatedText,
        string correctedText)
    {
        var mistakeCount = _textProcessingService.CountErrors(translatedText, correctedText);
        var userStatistics = await _userStatisticsRepository.Single(
            new UserStatisticsByUserIdSpecification(_userContextService.UserId));
        userStatistics.CorrectedMistakes += mistakeCount;
    }
}