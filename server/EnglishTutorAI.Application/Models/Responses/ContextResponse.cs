namespace EnglishTutorAI.Application.Models.Responses;

public class ContextResponse
{
    public bool IsAuthenticated { get; init; }

    public string FirstName { get; init; } = string.Empty;

    public string? Email { get; init; }

    public IList<string> RoleName { get; init; } = new List<string>();

    public string? Language { get; init; }
}