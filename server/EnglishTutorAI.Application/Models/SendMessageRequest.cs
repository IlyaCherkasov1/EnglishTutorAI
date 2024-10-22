using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EnglishTutorAI.Application.Models;

public class SendMessageRequest
{
    public required string Message { get; set; }
    public required string ThreadId { get; set; }

    public required string GroupId { get; set; }
}