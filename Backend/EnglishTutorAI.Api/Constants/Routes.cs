namespace EnglishTutorAI.Api.Constants;

public class Routes
{
    public const string Api = "/api";

    public static class Sentence
    {
        public const string GetSentence = "get-sentence";
    }

    public static class Story
    {
        public const string GetStoryByIndex = "get-story-by-index/{index}";

        public const string Count = "count";
    }

    public static class Assistant
    {
        public const string GenerateChatCompletion = "generate-chat-completion";
    }
}