namespace EnglishTutorAI.Domain.Entities;

public class UserStatistics : Entity
{
    public int CorrectedMistakes { get; set; }

    public Guid UserId { get; init; }

    public User User { get; set; } = null!;
}