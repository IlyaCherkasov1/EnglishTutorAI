using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;
using EnglishTutorAI.Domain.Enums;
using OpenAI.Assistants;
using OpenAI.Threads;

namespace EnglishTutorAI.Application.Interfaces;

public interface IAssistantClient
{
    Task<AssistantResponse> RetrieveAssistant(string assistantId);
    Task<ThreadResponse> CreateThread();
    Task AddMessageToThread(string threadId, string content);
    Task<RunResponse> CreateRunRequest(string assistantId, string threadId);
    Task<string> GenerateLastMessage(GenerateLastMessageModel model);
    Task<IReadOnlyList<ChatMessage>> GetAllMessages(string threadId, ChatType chatType);
}