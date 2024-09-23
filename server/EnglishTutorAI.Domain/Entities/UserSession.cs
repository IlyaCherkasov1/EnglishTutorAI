using EnglishTutorAI.Domain.Interfaces;

namespace EnglishTutorAI.Domain.Entities;

public class UserSession : Entity, IHasCreatedAt
{
    public required string RefreshToken { get; set; }

    public DateTime Expires { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsValid { get; set; } = true;

    public required Guid UserId { get; init; }

    public User User { get; init; } = null!;
}