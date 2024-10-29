namespace EnglishTutorAI.Application.Models.Responses;

public class ContextResponse
{
    public bool IsAuthenticated { get; init; }

    public required string FirstName { get; init; }

    public required IList<string> RoleName { get; init; }
}