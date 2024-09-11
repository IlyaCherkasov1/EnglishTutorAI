using EnglishTutorAI.Application.Models;

namespace EnglishTutorAI.Application.Interfaces;

public interface ISendAssistantMessageService
{
    public Task<string> SendMessageAndRun(SendMessageRequest request);
}