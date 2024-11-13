namespace EnglishTutorAI.Domain.Entities;

public class DocumentSentence : Entity
{
    public required string Text { get; init; }

    public required int Position { get; init; }

    public Guid DocumentId { get; init; }
}