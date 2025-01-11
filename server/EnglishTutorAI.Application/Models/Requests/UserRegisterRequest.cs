using System.ComponentModel.DataAnnotations;

namespace EnglishTutorAI.Application.Models.Requests;

public class UserRegisterRequest
{
    [MinLength(2)]
    public required string FirstName { get; set; }

    [EmailAddress]
    public required string Email { get; set; }

    [MinLength(6)]
    public required string Password { get; set; }
}