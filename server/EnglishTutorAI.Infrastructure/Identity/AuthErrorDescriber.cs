using Microsoft.AspNetCore.Identity;

namespace EnglishTutorAI.Infrastructure.Identity;

public class AuthErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DuplicateEmail(string email)
    {
        return new IdentityError { Code = "DuplicateEmail", Description = $"Email '{email}' is already registered." };
    }
}