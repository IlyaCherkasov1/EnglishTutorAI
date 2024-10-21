using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Domain.Entities;
using EnglishTutorAI.Domain.Enums;
using OpenAI.Assistants;

namespace EnglishTutorAI.Application.Interfaces;

public interface IAssistantClientService
{
    Task<AssistantThread> CreateThread();
    Task AddMessageToThread(string threadId, string content);
    Task CreateRunRequest(string assistantId, string threadId);
    Task<string> GetLastMessage(string threadId);
    Task<IEnumerable<DialogMessage>> GetAllMessages(string threadId);
}