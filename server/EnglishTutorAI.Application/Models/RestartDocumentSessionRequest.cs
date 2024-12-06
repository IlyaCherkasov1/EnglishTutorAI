namespace EnglishTutorAI.Application.Models;

public class RestartDocumentSessionRequest
{
    public Guid UserDocumentId { get; set; }

    public Guid CurrentSessionId { get; set; }
}