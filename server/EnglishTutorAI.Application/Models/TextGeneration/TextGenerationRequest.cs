namespace EnglishTutorAI.Application.Models.TextGeneration;

public class TextGenerationRequest
{
    public required string OriginalText { get; set; }

    public required string TranslatedText { get; set; }

    public required string ThreadId { get; set; }

    public Guid UserDocumentId { get; set; }
}