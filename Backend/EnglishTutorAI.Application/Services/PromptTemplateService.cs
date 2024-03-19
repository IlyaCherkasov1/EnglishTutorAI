using EnglishTutorAI.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace EnglishTutorAI.Application.Services;

public class PromptTemplateService : IPromptTemplateService
{
    private const string PromptPath = "prompt.json";
    public async Task<string> GetFormattedPromptAsync(string phrase)
    {
        var promptTemplate = await File.ReadAllTextAsync(PromptPath);
        var promptData = JsonConvert.DeserializeObject<Dictionary<string, string>>(promptTemplate);

        if (promptData != null && promptData.TryGetValue("template", out var template))
        {
            return template.Replace("{phrase}", phrase);
        }

        throw new FileNotFoundException("The prompt template could not be found or is invalid.");
    }
}