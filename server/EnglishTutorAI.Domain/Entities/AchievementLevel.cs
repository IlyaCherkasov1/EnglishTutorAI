namespace EnglishTutorAI.Domain.Entities;

public class AchievementLevel : Entity
{
    public int Goal { get; init; }

    public Guid AchievementId { get; init; }

    public Achievement Achievement { get; init; } = null!;
}