using System.Text;
using System.Text.RegularExpressions;
using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public partial class TextComparisonService : ITextComparisonService
{
    [GeneratedRegex(@"\p{C}+")]
    private static partial Regex InvisibleCharsRegex();

    private static readonly char[] Separator = [' ', '.', ',', ';', ':', '!', '?', '\'', '\"', '(', ')'];

    public bool HasTextChanged(string originalText, string correctedText)
    {
        var originalWords = originalText.Split(Separator, StringSplitOptions.RemoveEmptyEntries)
            .Select(CleanString)
            .ToList();

        var correctedWords = correctedText.Split(Separator, StringSplitOptions.RemoveEmptyEntries)
            .Select(CleanString)
            .ToList();

        if (originalWords.Count != correctedWords.Count)
        {
            return true;
        }

        return originalWords.Where((t, i) => !t.Equals(correctedWords[i], StringComparison.OrdinalIgnoreCase)).Any();
    }

    private static string CleanString(string input)
    {
        return InvisibleCharsRegex().Replace(input, "").Normalize(NormalizationForm.FormC);
    }
}