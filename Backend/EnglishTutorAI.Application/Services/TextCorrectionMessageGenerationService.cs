using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models.TextGeneration;

namespace EnglishTutorAI.Application.Services;

public class TextCorrectionMessageGenerationService : ITextCorrectionMessageGenerationService
{
    private const string TranslatedTextPlaceholder = "{TranslatedText}";
    private const string OriginalTextPlaceholder = "{OriginalText}";
    private const string TemplateKey = "textCorrectionTemplate";

    private readonly IMessageGenerationService _messageGenerationService;

    public TextCorrectionMessageGenerationService(IMessageGenerationService messageGenerationService)
    {
        _messageGenerationService = messageGenerationService;
    }

    public async Task<string> GenerateMessageAsync(TextGenerationRequest request)
    {
        var placeholderValues = new Dictionary<string, string>
        {
            { TranslatedTextPlaceholder, request.TranslatedText },
            { OriginalTextPlaceholder, request.OriginalText }
        };

        return await _messageGenerationService.Generate(placeholderValues, TemplateKey);
    }
}