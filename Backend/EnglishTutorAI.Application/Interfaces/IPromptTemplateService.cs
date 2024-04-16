using EnglishTutorAI.Application.Models;

namespace EnglishTutorAI.Application.Interfaces;

public interface IPromptTemplateService
{
    Task<string> GetFormattedPromptAsync(Dictionary<string, string> placeholderValues, string templateKey);
}