namespace EnglishTutorAI.Application.Interfaces;

public interface IOpenAiService
{
    Task<string> GenerateSentences(string text);
}