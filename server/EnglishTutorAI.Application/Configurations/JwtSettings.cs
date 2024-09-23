namespace EnglishTutorAI.Application.Configurations;

public class JwtSettings
{
    public required string Key { get; init; }

    public required string Issuer { get; init; }

    public required string Audience { get; init; }

    public required string AccessTokenExpiryMinutes { get; init; }
}