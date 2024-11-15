namespace EnglishTutorAI.Domain.Entities;

public class Achievement : Entity
{
    public required string Name { get; init; }

    public required string Description { get; init; }

    public bool IsCompleted { get; set; }

    public required string IconFileName { get; init; }

    public ICollection<AchievementLevel> AchievementLevels { get; init; } = null!;
}