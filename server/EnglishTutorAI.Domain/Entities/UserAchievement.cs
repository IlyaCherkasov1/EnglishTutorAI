namespace EnglishTutorAI.Domain.Entities;

public class UserAchievement : Entity
{
    public Guid UserId { get; init; }

    public Guid AchievementId { get; init; }

    public int Progress { get; set; }

    public int CurrentLevel { get; set; }

    public Achievement Achievement { get; set; } = null!;
}