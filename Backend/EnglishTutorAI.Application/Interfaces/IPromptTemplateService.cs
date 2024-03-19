namespace EnglishTutorAI.Application.Interfaces;

public interface IPromptTemplateService
{
    Task<string> GetFormattedPromptAsync(string phrase);
}