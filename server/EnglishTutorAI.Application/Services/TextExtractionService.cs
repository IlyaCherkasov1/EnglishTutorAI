using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class TextExtractionService : ITextExtractionService
{
    public string ExtractCleanText(string correctedText, string originalText)
    {
        var startIndex = correctedText.IndexOf(originalText, StringComparison.OrdinalIgnoreCase);

        if (startIndex != -1)
        {
            var endIndex = startIndex + originalText.Length;
            return correctedText.Substring(startIndex, endIndex - startIndex).Trim();
        }

        return correctedText.Trim();
    }
}