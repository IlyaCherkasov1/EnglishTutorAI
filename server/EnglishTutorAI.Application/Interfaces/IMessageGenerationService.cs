using EnglishTutorAI.Application.Models;

namespace EnglishTutorAI.Application.Interfaces;

public interface IMessageGenerationService
{
    Task<string> Generate(Dictionary<string, string> placeholderValues, string templateKey);
}