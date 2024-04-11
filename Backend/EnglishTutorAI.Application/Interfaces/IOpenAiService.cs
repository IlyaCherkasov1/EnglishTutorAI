namespace EnglishTutorAI.Application.Interfaces;

public interface IOpenAiService
{
    Task<string> GenerateChatCompletion(string text);
}