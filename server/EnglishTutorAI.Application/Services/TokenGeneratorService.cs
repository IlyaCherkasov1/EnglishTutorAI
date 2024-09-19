using System.Security.Cryptography;
using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class TokenGeneratorService : ITokenGeneratorService
{
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[128];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }
}