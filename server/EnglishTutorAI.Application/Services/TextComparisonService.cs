using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class TextComparisonService : ITextComparisonService
{
    private static readonly char[] Separator = [' ', '.', ',', ';', ':', '!', '?'];

    public bool HasTextChanged(string originalText, string correctedText)
    {
        var originalWords = originalText.Split(Separator, StringSplitOptions.RemoveEmptyEntries);
        var correctedWords = correctedText.Split(Separator, StringSplitOptions.RemoveEmptyEntries);

        if (originalWords.Length != correctedWords.Length)
        {
            return true;
        }

        return originalWords.Where((t, i) => !t.Equals(correctedWords[i], StringComparison.OrdinalIgnoreCase)).Any();
    }
}