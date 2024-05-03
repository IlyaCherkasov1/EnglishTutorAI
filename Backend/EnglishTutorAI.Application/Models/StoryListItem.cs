namespace EnglishTutorAI.Application.Models;

public class StoryListItem
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public DateTime CreatedAt { get; set; }
}