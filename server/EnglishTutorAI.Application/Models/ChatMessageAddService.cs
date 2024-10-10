using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Models;

[ScopedDependency]
public class ChatMessageAddService : IChatMessageAddService
{
    private readonly IRepository<ChatMessage> _chatMessageRepository;

    public ChatMessageAddService(IRepository<ChatMessage> chatMessageRepository)
    {
        _chatMessageRepository = chatMessageRepository;
    }

    public async Task Add(AddChatMessageModel model)
    {
        var chatMessage = new ChatMessage
        {
            Content = model.Content,
            ThreadId = model.ThreadId,
            ConversationRole = model.Role,
            ChatType = model.ChatType,
        };

        await _chatMessageRepository.Add(chatMessage);
    }
}