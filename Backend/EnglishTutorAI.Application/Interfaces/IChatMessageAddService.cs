using EnglishTutorAI.Application.Models;

namespace EnglishTutorAI.Application.Interfaces;

public interface IChatMessageAddService
{
    Task Add(AddChatMessageModel model);
}