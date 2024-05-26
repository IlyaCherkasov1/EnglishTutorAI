namespace EnglishTutorAI.Application.Models.TextGeneration;

public class TextGenerationRequest : CreateAssistantResponse
{
    public string? OriginalText { get; set; }
    public string? TranslatedText { get; set; }
}