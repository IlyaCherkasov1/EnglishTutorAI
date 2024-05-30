using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EnglishTutorAI.Application.Models;

public class SendMessageRequest : ThreadCreationResponse
{
    [Required]
    public string Message { get; set; } = string.Empty;
}