using EnglishTutorAI.Application.Models;

namespace EnglishTutorAI.Application.Interfaces;

public interface IPromptTemplateService
{
    Task<string> GetFormattedPromptAsync(PromptParameters parameters);
}