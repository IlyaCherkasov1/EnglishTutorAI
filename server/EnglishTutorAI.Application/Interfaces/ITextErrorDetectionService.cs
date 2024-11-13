namespace EnglishTutorAI.Application.Interfaces;

public interface ITextErrorDetectionService
{
    int CountGroupedErrors(string originalText, string correctedText);
}