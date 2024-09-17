using EnglishTutorAI.Domain.Interfaces;

namespace EnglishTutorAI.Domain.Entities;

public class RefreshToken : Entity, IHasCreatedAt
{
    public required string Token { get; set; }

    public DateTime Expires { get; set; }

    public bool IsExpired => DateTime.UtcNow >= Expires;

    public DateTime CreatedAt { get; set; }

    public required Guid UserId { get; init; }

    public User User { get; init; } = null!;
}