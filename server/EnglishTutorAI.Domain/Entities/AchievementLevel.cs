namespace EnglishTutorAI.Domain.Entities;

public class AchievementLevel : Entity
{
    public Guid AchievementId { get; init; }

    public int Goal { get; init; }

    public Achievement Achievement { get; init; } = null!;
}