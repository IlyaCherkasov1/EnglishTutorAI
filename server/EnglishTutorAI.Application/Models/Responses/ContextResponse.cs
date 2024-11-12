namespace EnglishTutorAI.Application.Models.Responses;

public class ContextResponse
{
    public Guid UserId { get; set; }
    public bool IsAuthenticated { get; init; }

    public  string FirstName { get; init; } = string.Empty;

    public IList<string> RoleName { get; init; } = new List<string>();
}