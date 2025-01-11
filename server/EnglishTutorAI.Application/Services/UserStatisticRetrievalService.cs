using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class UserStatisticRetrievalService : IUserStatisticRetrievalService
{
    private readonly IRepository<UserStatistics> _userStatisticsRepository;
    private readonly IUserContextService _userContextService;

    public UserStatisticRetrievalService(
        IRepository<UserStatistics> userStatisticsRepository,
        IUserContextService userContextService)
    {
        _userStatisticsRepository = userStatisticsRepository;
        _userContextService = userContextService;
    }

    public async Task<UserStatisticsResponse> Retrieve()
    {
        var result = await _userStatisticsRepository.Single(
            new UserStatisticsByUserIdSpecification(_userContextService.UserId));

        return new UserStatisticsResponse
        {
            CorrectedMistakes = result.CorrectedMistakes
        };
    }
}