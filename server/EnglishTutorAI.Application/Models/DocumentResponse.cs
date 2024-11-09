
namespace EnglishTutorAI.Application.Models;

public class DocumentResponse
{
    public Guid Id { get; init; }

    public required string Title { get; init; }

    public required IEnumerable<string> Sentences { get; set; }

    public DateTime CreatedAt { get; init; }

    public required string ThreadId { get; init; }

    public int CurrentLine { get; init; }

    public Guid SessionId { get; set; }
}