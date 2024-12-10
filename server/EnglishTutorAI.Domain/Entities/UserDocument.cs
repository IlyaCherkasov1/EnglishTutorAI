namespace EnglishTutorAI.Domain.Entities;

public class UserDocument : Entity
{
    public int CurrentLine { get; set; }

    public required string ThreadId { get; init; }

    public User? User { get; init; }

    public required Guid UserId { get; init; }

    public required bool IsCompleted { get; set; }

    public DateTime CompletedOn { get; set; }

    public Guid SessionId { get; set; }

    public ICollection<LinguaFixMessage> LinguaFixMessages { get; set; } = null!;

    public required Guid DocumentId { get; init; }

    public Document Document { get; init; } = null!;
}