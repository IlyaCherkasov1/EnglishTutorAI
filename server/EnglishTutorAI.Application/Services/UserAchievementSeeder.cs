using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class UserAchievementSeeder : IUserAchievementSeeder
{
    private readonly IRepository<UserAchievement> _userAchievementRepository;
    private readonly IRepository<Achievement> _achievementRepository;

    public UserAchievementSeeder(
        IRepository<UserAchievement> userAchievementRepository,
        IRepository<Achievement> achievementRepository)
    {
        _userAchievementRepository = userAchievementRepository;
        _achievementRepository = achievementRepository;
    }

    public async Task Seed(User user)
    {
        var allAchievements = await _achievementRepository.ListAll();

        var newUserAchievements = allAchievements.Select(a => new UserAchievement
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            AchievementId = a.Id,
            Progress = 0,
            CurrentLevel = 0,
        }).ToList();

        if (newUserAchievements.Count != 0)
        {
            await _userAchievementRepository.Add(newUserAchievements);
        }
    }
}