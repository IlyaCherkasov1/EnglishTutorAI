namespace EnglishTutorAI.Application.Models.TextGeneration;

public class TextGenerationRequest : ThreadCreationResponse
{
    public string? OriginalText { get; set; }
    public string? TranslatedText { get; set; }
}