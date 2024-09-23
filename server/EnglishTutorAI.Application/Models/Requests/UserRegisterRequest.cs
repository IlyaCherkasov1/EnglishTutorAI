using System.ComponentModel.DataAnnotations;

namespace EnglishTutorAI.Application.Models.Requests;

public class UserRegisterRequest
{
    public required string FirstName { get; set; }

    [EmailAddress]
    public required string Email { get; set; }

    public required string Password { get; set; }
}