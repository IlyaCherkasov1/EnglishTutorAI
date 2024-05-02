using EnglishTutorAI.Application.Models.TextGeneration;

namespace EnglishTutorAI.Application.Interfaces;

public interface IOpenAiService
{
    Task<string> GenerateChatCompletion(TextGenerationRequest request);
}