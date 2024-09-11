namespace EnglishTutorAI.Application.Models;

public class JwtSettings
{
    public required string Key { get; init; }

    public required string Audience { get; init; }

    public required string Issuer { get; init; }
}