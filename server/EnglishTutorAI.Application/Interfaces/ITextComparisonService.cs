namespace EnglishTutorAI.Application.Interfaces;

public interface ITextComparisonService
{
    bool HasTextChanged(string originalText, string correctedText);
}