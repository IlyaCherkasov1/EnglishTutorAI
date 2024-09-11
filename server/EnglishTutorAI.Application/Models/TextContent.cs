using System.Text.Json.Serialization;

namespace EnglishTutorAI.Application.Models;

public class TextContent
{
    [JsonPropertyName("value")]
    public string Value { get; set; }

    [JsonPropertyName("annotations")]
    public List<object> Annotations { get; set; }
}