namespace EnglishTutorAI.Application.Models;

public class RestartDocumentSessionRequest
{
    public Guid DocumentId { get; set; }

    public Guid CurrentSessionId { get; set; }
}