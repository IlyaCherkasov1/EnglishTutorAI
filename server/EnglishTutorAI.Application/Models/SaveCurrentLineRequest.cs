namespace EnglishTutorAI.Application.Models;

public class SaveCurrentLineRequest
{
    public int CurrentLine { get; set; }
    public Guid UserDocumentId { get; set; }
}