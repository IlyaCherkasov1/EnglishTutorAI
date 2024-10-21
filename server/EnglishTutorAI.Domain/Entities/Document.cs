using EnglishTutorAI.Domain.Enums;
using EnglishTutorAI.Domain.Interfaces;

namespace EnglishTutorAI.Domain.Entities;

public class Document : Entity, IHasCreatedAt
{
    public required string Title { get; set; }

    public required string Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public required string ThreadId { get; set; }

    public int CurrentLine { get; set; }

    public required StudyTopic StudyTopic { get; init; }

    public ICollection<LinguaFixMessage> LinguaFixMessages { get; } =  new List<LinguaFixMessage>();
}