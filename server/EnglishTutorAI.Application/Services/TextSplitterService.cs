using System.Text;
using System.Text.RegularExpressions;
using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public partial class TextSplitterService : ITextSplitterService
{
    [GeneratedRegex(@"\p{C}+")]
    private static partial Regex InvisibleCharsRegex();

    private static readonly char[] Separator = [' ', '.', ',', ';', ':', '!', '?', '\'', '\"', '(', ')'];


    public List<string> SplitText(string text)
    {
        return text.Split(Separator, StringSplitOptions.RemoveEmptyEntries)
            .Select(CleanString)
            .ToList();
    }

    private static string CleanString(string input)
    {
        return InvisibleCharsRegex().Replace(input, "").Normalize(NormalizationForm.FormC);
    }
}