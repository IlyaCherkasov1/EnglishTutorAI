
namespace EnglishTutorAI.Application.Models;

public class DocumentResponse
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? ThreadId { get; set; }

    public int CurrentLine { get; set; }
}