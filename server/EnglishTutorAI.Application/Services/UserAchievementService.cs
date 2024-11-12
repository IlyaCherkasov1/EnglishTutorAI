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

    public UserAchievementService(IRepository<UserAchievement> userAchievementRepository)
    {
        _userAchievementRepository = userAchievementRepository;
    }

    public async Task<IEnumerable<UserAchievementResponse>> GetUserAchievements(Guid userId)
    {
        return await _userAchievementRepository.List(new UserAchievementByUserIdSpecification(userId));
    }
}