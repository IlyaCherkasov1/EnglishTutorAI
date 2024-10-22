using EnglishTutorAI.Application.Models;

namespace EnglishTutorAI.Application.Interfaces;

public interface ISendAssistantMessageService
{
    Task<string> SendMessageAndSaveToTheRepository(SendMessageRequest request);

    Task<string> SendMessage(SendMessageRequest request);
}