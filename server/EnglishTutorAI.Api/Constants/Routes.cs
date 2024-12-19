namespace EnglishTutorAI.Api.Constants;

public static class Routes
{
    public const string Api = "/api";

    public static class Translate
    {
        public const string AddTranslate = "add-translate";

        public const string GetTranslate = "get-translates";

        public const string GetTranslateDetails = "get-translate-details/{id}";

        public const string SaveCurrentLine = "save-current-line";

        public const string GetConversationThread = "get-conversation-thread/{threadId}";

        public const string DeleteTranslate = "delete-translate/{translateId}";

        public const string HandleTranslateCompletion = "handle-translate-completion/{userTranslateId}";

        public const string HandleTranslateStart = "handle-translate-start/{userTranslateId}";

        public const string GetNextTranslate = "get-next-translate";

        public const string GetCompletedTranslates = "get-completed-translates";
    }

    public static class TranslateHistory
    {
        public const string GetMistakesHistory = "get-mistake-history-items";

        public const string GetTranslateSessionMistakesHistory = "get-session-mistake-history/{userTranslateId}";
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

    public static class Achievements
    {
        public const string GetAchievements = "get-achievements";
    }

    public static class UserStatistics
    {
        public const string GetStatistics = "get-statistics";
    }

    public static class User
    {
        public const string ChangeLanguage = "change-language";
    }
}