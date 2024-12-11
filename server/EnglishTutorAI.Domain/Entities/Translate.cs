using EnglishTutorAI.Domain.Enums;
using EnglishTutorAI.Domain.Interfaces;

namespace EnglishTutorAI.Domain.Entities;

public class Translate : Entity, IHasCreatedAt
{
    public required string Title { get; init; }

    public DateTime CreatedAt { get; set; }

    public required StudyTopic StudyTopic { get; init; }

    public ICollection<TranslateSentence> Sentences { get; set; } = null!;

    public ICollection<UserTranslate> UserTranslates { get; set; } = null!;
}