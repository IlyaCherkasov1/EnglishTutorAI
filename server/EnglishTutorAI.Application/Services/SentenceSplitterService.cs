using System.Text.RegularExpressions;
using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public partial class SentenceSplitterService : ISentenceSplitterService
{
    [GeneratedRegex(@"(?<=[.!?])\s+(?=[A-ZА-ЯЁ])")]
    private static partial Regex MyRegex();

    public IEnumerable<string> Split(string text)
    {
        var matches = MyRegex().Split(text);
        var sentences = matches.Select(match => match.Trim());

        return sentences;
    }
}