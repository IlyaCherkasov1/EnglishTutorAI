namespace EnglishTutorAI.Application.Models;

public class SendMessageRequest : ThreadCreationResponse
{
    public string? Message { get; set; }
}