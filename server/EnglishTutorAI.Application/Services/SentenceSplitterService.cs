using System.Text.RegularExpressions;
using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class SentenceSplitterService : ISentenceSplitterService
{
    public Task<IEnumerable<string>> Split(string text)
    {
        var pattern = @"(?<=[.!?])\s+(?=[A-ZА-ЯЁ])";
        var matches = Regex.Split(text, pattern);
        var sentences = matches.Select(match => match.Trim());

        return Task.FromResult(sentences);
    }
}