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
    private readonly IAuthenticatedUserContext _authenticatedUserContext;

    public UserStatisticRetrievalService(
        IRepository<UserStatistics> userStatisticsRepository,
        IAuthenticatedUserContext authenticatedUserContext)
    {
        _userStatisticsRepository = userStatisticsRepository;
        _authenticatedUserContext = authenticatedUserContext;
    }

    public async Task<UserStatisticsResponse> Retrieve()
    {
        var userId = _authenticatedUserContext.UserId!.Value;
        var result = await _userStatisticsRepository.Single(new UserStatisticsByUserIdSpecification(userId));

        return new UserStatisticsResponse
        {
            CorrectedErrors = result.CorrectedMistakes
        };
    }
}