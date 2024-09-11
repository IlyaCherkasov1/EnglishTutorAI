using EnglishTutorAI.Application.Models.TextGeneration;

namespace EnglishTutorAI.Application.Interfaces;

public interface ITextCorrectionMessageGenerationService
{
    Task<string> GenerateMessageAsync(TextGenerationRequest request);
}