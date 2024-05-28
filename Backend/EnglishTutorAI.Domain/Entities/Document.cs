using EnglishTutorAI.Domain.Interfaces;

namespace EnglishTutorAI.Domain.Entities;

public class Document : Entity, IHasCreatedAt
{
    public string? Title { get; set; }

    public string? Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? ThreadId { get; set; }

    public int CurrentLine { get; set; }
}