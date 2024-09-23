using EnglishTutorAI.Domain.Enums;

namespace EnglishTutorAI.Application.Models;

public record AddChatMessageModel(string ThreadId, string Content, ChatType ChatType, ConversationRole Role);