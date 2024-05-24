using EnglishTutorAI.Application.Models;
using OpenAI.Assistants;
using OpenAI.Threads;

namespace EnglishTutorAI.Application.Interfaces;

public interface IAssistantClient
{
    Task<AssistantResponse> RetrieveAssistant(string assistantId);
    Task<ThreadResponse> CreateThread();
    Task AddMessageToThread(string threadId, string content);
    Task<RunResponse> CreateRunRequest(string assistantId, string threadId);
    Task<string> GetLastMessage(RunResponse run);
    Task<List<MessageHistoryItem>> GetAllMessages(RunResponse run);
}