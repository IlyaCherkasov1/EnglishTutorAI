using System.Text;
using System.Text.RegularExpressions;
using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class TextComparisonService : ITextComparisonService
{
    private readonly ITextSplitterService _textSplitterService;

    public TextComparisonService(ITextSplitterService textSplitterService)
    {
        _textSplitterService = textSplitterService;
    }

    public bool HasTextChanged(string originalText, string correctedText)
    {
        var originalWords = _textSplitterService.SplitText(originalText);
        var correctedWords = _textSplitterService.SplitText(correctedText);

        if (originalWords.Count != correctedWords.Count)
        {
            return true;
        }

        return originalWords.Where((t, i) =>
            !t.Equals(correctedWords[i], StringComparison.OrdinalIgnoreCase)).Any();
    }
}