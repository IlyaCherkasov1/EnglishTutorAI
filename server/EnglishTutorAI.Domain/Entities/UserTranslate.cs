namespace EnglishTutorAI.Domain.Entities;

public class UserTranslate : Entity
{
    public int CurrentLine { get; set; }

    public required string ThreadId { get; init; }

    public User? User { get; init; }

    public required Guid UserId { get; init; }

    public required bool IsCompleted { get; set; }

    public DateTime? CompletedOn { get; set; }

    public Guid SessionId { get; set; }

    public ICollection<LinguaFixMessage> LinguaFixMessages { get; set; } = null!;

    public required Guid TranslateId { get; init; }

    public Translate Translate { get; init; } = null!;
}