using System.ComponentModel.DataAnnotations;

namespace EnglishTutorAI.Application.Models;

public class LoginData
{
    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}