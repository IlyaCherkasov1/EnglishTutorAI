using Microsoft.AspNetCore.Identity;

namespace EnglishTutorAI.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public required string FirstName { get; init; }

    public UserStatistics UserStatistics { get; init; } = null!;

    public ICollection<UserDocument> UserDocuments { get; init; } = null!;
}