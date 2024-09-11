using EnglishTutorAI.Application.Models.TextGeneration;

namespace EnglishTutorAI.Application.Interfaces;

public interface ITextCorrectionService
{
    Task<(bool IsCorrected, string CorrectedText)> Correct(TextGenerationRequest request);
}