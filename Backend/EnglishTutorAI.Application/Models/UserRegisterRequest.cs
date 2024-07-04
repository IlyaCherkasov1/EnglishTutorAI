namespace EnglishTutorAI.Application.Models;

public class UserRegisterRequest
{
    public required string FirstName { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
}