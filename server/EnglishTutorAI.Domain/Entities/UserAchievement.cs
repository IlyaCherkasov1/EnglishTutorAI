namespace EnglishTutorAI.Domain.Entities;

public class UserAchievement : Entity
{
    public int Progress { get; set; }

    public int CurrentLevel { get; set; }

    public Guid AchievementId { get; init; }
    public Achievement Achievement { get; set; } = null!;

    public Guid UserId { get; init; }

    public User User { get; init; } = null!;
}