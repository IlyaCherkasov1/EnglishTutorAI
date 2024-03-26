using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using Newtonsoft.Json;

namespace EnglishTutorAI.Application.Services;

public class PromptTemplateService : IPromptTemplateService
{
    private const string PromptPath = "prompt.json";
    public async Task<string> GetFormattedPromptAsync(PromptParameters parameters)
    {
        var promptTemplate = await File.ReadAllTextAsync(PromptPath);
        var promptData = JsonConvert.DeserializeObject<Dictionary<string, string>>(promptTemplate);

        if (promptData != null && promptData.TryGetValue(parameters.TemplateKey!, out var template))
        {
            return template.Replace(parameters.Placeholder!, parameters.Text);
        }

        throw new FileNotFoundException("The prompt template could not be found or is invalid.");
    }
}