using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class TextProcessingService : ITextProcessingService
{
    private readonly ITextComparisonService _textComparisonService;
    private readonly ITextExtractionService _textExtractionService;
    private readonly ITextErrorDetectionService _textErrorDetectionService;

    public TextProcessingService(
        ITextComparisonService textComparisonService,
        ITextExtractionService textExtractionService,
        ITextErrorDetectionService textErrorDetectionService)
    {
        _textComparisonService = textComparisonService;
        _textExtractionService = textExtractionService;
        _textErrorDetectionService = textErrorDetectionService;
    }

    public bool HasTextChanged(string originalText, string newText) =>
        _textComparisonService.HasTextChanged(originalText, newText);

    public string ExtractCleanText(string correctedText, string originalText) =>
        _textExtractionService.ExtractCleanText(correctedText, originalText);

    public int CountErrors(string originalText, string correctedText) =>
        _textErrorDetectionService.CountGroupedErrors(originalText, correctedText);
}