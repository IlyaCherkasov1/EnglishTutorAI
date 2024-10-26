namespace EnglishTutorAI.Api.Constants;

public static class Routes
{
    public const string Api = "/api";

    public static class Document
    {
        public const string AddDocument = "add-document";

        public const string GetDocument = "get-documents";

        public const string GetDocumentDetails = "get-document-details/{id}";

        public const string SplitDocumentContent = "split-document-content";

        public const string SaveCurrentLine = "save-current-line";

        public const string GetConversationThread = "get-conversation-thread/{threadId}";

        public const string DeleteDocument = "delete-document/{documentId}";
    }

    public static class Assistant
    {
        public const string GenerateChatCompletion = "correct-text";
        public const string SendMessage = "send-message";
        public const string SendMessageWithSave = "send-message-with-save";
    }

    public static class Identity
    {
        public const string Register = "register";

        public const string Login = "login";

        public const string RenewAccessToken = "renewAccessToken";

        public const string Logout = "logout";
    }

    public static class ExternalAuth
    {
        public const string ExternalLogin = "external-login";

        public const string ExternalLoginCallback = "external-auth-callback";
    }

    public static class Urls
    {
        public const string ExternalAuthCallbackUrl = "https://localhost:7008/api/externalAuth/external-auth-callback";
    }
}