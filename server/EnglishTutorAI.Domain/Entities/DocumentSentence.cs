namespace EnglishTutorAI.Domain.Entities;

public class DocumentSentence : Entity
{
    public Guid DocumentId { get; init; }

    public required string Text { get; init; }

    public required int Position { get; init; }
}