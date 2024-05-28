namespace EnglishTutorAI.Api.Constants;

public class Routes
{
    public const string Api = "/api";

    public static class Sentence
    {
        public const string GetSentence = "get-sentence";
    }

    public static class Document
    {
        public const string GetDocumentByIndex = "get-document-by-index/{index}";

        public const string Count = "count";

        public const string AddDocument = "add-document";

        public const string GetDocument = "get-documents";

        public const string GetDocumentDetails = "get-document-details/{id}";

        public const string SplitDocumentContent = "split-document-content";
        public const string SaveCurrentLine = "save-current-line";
    }

    public static class Assistant
    {
        public const string GenerateChatCompletion = "correct-text";
        public const string Create = "create-thread/{documentId}";
        public const string SendMessage = "send-message";
    }
}