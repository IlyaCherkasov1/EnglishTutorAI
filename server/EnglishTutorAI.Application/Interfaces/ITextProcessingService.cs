namespace EnglishTutorAI.Application.Interfaces;

public interface ITextProcessingService
{
    bool HasTextChanged(string originalText, string newText);

    string ExtractCleanText(string correctedText, string originalText);

    int CountErrors(string originalText, string correctedText);
}