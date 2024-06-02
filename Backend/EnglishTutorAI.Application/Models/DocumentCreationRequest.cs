namespace EnglishTutorAI.Application.Models;

public class DocumentCreationRequest
{
    public required string Title { get; set; }
    public required string Content { get; set; }
}