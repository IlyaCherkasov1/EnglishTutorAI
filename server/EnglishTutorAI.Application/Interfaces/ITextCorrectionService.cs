using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.TextGeneration;

namespace EnglishTutorAI.Application.Interfaces;

public interface ITextCorrectionService
{
    Task<TextCorrectionResult> Correct(TextGenerationRequest request);
}