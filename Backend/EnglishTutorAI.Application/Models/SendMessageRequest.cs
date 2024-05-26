namespace EnglishTutorAI.Application.Models;

public class SendMessageRequest : CreateAssistantResponse
{
    public string? Message { get; set; }
}