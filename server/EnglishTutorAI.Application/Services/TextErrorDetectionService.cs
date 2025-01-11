using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class TextErrorDetectionService : ITextErrorDetectionService
{
    private readonly ITextSplitterService _textSplitterService;

    public TextErrorDetectionService(ITextSplitterService textSplitterService)
    {
        _textSplitterService = textSplitterService;
    }

    public int CountGroupedErrors(string originalText, string correctedText)
    {
        var originalWords = _textSplitterService.SplitText(originalText);
        var correctedWords = _textSplitterService.SplitText(correctedText);

        var originalLength = originalWords.Count;
        var correctedLength = correctedWords.Count;

        var dp = new int[originalLength + 1, correctedLength + 1];

        for (var i = 0; i <= originalLength; i++)
        {
            dp[i, 0] = i;
        }

        for (var j = 0; j <= correctedLength; j++)
        {
            dp[0, j] = j;
        }

        for (var i = 1; i <= originalLength; i++)
        {
            for (var j = 1; j <= correctedLength; j++)
            {
                if (originalWords[i - 1] == correctedWords[j - 1])
                {
                    dp[i, j] = dp[i - 1, j - 1];
                }
                else
                {
                    dp[i, j] = Math.Min(dp[i - 1, j - 1],
                        Math.Min(dp[i - 1, j],
                            dp[i, j - 1])) + 1;
                }
            }
        }

        var errors = 0;
        var inErrorSequence = false;

        int x = originalLength, y = correctedLength;
        while (x > 0 || y > 0)
        {
            if (x > 0 && y > 0 && originalWords[x - 1] == correctedWords[y - 1])
            {
                x--;
                y--;
                inErrorSequence = false;
            }
            else
            {
                if (!inErrorSequence)
                {
                    errors++;
                    inErrorSequence = true;
                }

                if (x > 0 && y > 0 && dp[x, y] == dp[x - 1, y - 1] + 1)
                {
                    x--;
                    y--;
                }
                else if (x > 0 && dp[x, y] == dp[x - 1, y] + 1)
                {
                    x--;
                }
                else if (y > 0 && dp[x, y] == dp[x, y - 1] + 1)
                {
                    y--;
                }
            }
        }

        return errors;
    }
}