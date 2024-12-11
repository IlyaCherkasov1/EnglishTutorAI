namespace EnglishTutorAI.Domain.Entities;

public class TranslateSentence : Entity
{
    public required string Text { get; init; }

    public required int Position { get; init; }

    public Guid TranslateId { get; init; }
}