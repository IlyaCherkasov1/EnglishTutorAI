using EnglishTutorAI.Application.Models.TextGeneration;

namespace EnglishTutorAI.Application.Interfaces;

public interface IAssistantMessageService
{
    Task GenerateAndAddMessageAsync(TextGenerationRequest request, string threadId);

    Task<string> GetCorrectedMessageAsync(string originalText, string threadId);
}