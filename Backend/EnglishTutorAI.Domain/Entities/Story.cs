namespace EnglishTutorAI.Domain.Entities;

public class Story : Entity
{
    public string? Title { get; set; }

    public string? Content { get; set; }

    public DateTime CreatedAt { get; set; }
}