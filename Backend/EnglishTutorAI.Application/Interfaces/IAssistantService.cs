namespace EnglishTutorAI.Application.Interfaces;

public interface IAssistantService
{
    public Task<string> StartConversation(string message);
}