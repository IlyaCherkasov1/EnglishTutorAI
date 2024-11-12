namespace EnglishTutorAI.Application.Models;

public class UserAchievementResponse
{
    public int Progress { get; set; }

    public int CurrentLevel { get; set; }

    public required string Name { get; init; }

    public required string Description { get; init; }

    public required List<int> LevelGoals { get; init; }

    public bool IsCompleted { get; init; }

    public required string IconFileName { get; init; }
}