namespace EnglishTutorAI.Application.Interfaces;

public interface IUserManagementService
{
    Task UpdateStatistics(Guid userId, int mistakeCount);

    Task UpdateAchievement(Guid userId, Guid achievementId);
}