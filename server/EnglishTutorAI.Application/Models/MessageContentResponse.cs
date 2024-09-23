using System.Text.Json.Serialization;

namespace EnglishTutorAI.Application.Models;

public class MessageContentResponse
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("text")]
    public TextContent Text { get; set; }
}