namespace EnglishTutorAI.Application.Interfaces;

public interface IOpenAiService
{
    Task GenerateSentences(string phrase);
}