namespace EnglishTutorAI.Application.Models;

public class DocumentListItem
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public DateTime CreatedAt { get; set; }

    public required string Content { get; set; }

    public required string StudyTopic { get; init; }
}