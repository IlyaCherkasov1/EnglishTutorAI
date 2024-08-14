namespace EnglishTutorAI.Application.Configurations;

public class AwsSettings
{
    public required string Profile { get; init; }
    public required string Region { get; init; }
    public required string AccessKey { get; init; }
    public required string SecretKey { get; init; }
}