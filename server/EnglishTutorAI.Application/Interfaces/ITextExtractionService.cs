namespace EnglishTutorAI.Application.Interfaces;

public interface ITextExtractionService
{
    string ExtractCleanText(string correctedText, string originalText);
}