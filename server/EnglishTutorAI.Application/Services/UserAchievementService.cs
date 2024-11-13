using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class UserAchievementService : IUserAchievementService
{
    private readonly IRepository<UserAchievement> _userAchievementRepository;
    private readonly IAuthenticatedUserContext _authenticatedUserContext;

    public UserAchievementService(
        IRepository<UserAchievement> userAchievementRepository,
        IAuthenticatedUserContext authenticatedUserContext)
    {
        _userAchievementRepository = userAchievementRepository;
        _authenticatedUserContext = authenticatedUserContext;
    }

    public async Task<IEnumerable<UserAchievementResponse>> GetUserAchievements()
    {
        var userId = _authenticatedUserContext.UserId!.Value;

        return await _userAchievementRepository.List(new UserAchievementByUserIdSpecification(userId));
    }
}