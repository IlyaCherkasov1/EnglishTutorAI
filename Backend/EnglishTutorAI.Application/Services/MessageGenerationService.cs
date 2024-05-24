using EnglishTutorAI.Application.Interfaces;
using Newtonsoft.Json;

namespace EnglishTutorAI.Application.Services;

public class MessageGenerationService : IMessageGenerationService
{
    private const string PromptPath = "prompt.json";

    public async Task<string> Generate(Dictionary<string, string> placeholderValues, string templateKey)
    {
        var promptTemplate = await File.ReadAllTextAsync(PromptPath);
        var promptData = JsonConvert.DeserializeObject<Dictionary<string, string>>(promptTemplate);
        var result = string.Empty;

        if (promptData == null || !promptData.TryGetValue(templateKey, out var template))
        {
            return result;
        }

        return placeholderValues.Aggregate(template, (current, value) => current.Replace(value.Key, value.Value));
    }
}