
namespace EnglishTutorAI.Application.Models;

public class DocumentResponse
{
    public Guid Id { get; init; }

    public required string Title { get; init; }

    public required string Content { get; init; }

    public DateTime CreatedAt { get; init; }

    public required string ThreadId { get; init; }

    public int CurrentLine { get; init; }
}