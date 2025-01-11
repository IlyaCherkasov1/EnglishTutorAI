using System.ComponentModel.DataAnnotations;

namespace EnglishTutorAI.Application.Models.TextGeneration;

public class TextGenerationRequest
{
    [Required]
    public required string OriginalText { get; set; }

    [Required]
    public required string TranslatedText { get; set; }

    public required string ThreadId { get; set; }

    public Guid UserTranslateId { get; set; }
}