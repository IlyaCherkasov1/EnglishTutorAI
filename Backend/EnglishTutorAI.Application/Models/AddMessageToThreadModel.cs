using EnglishTutorAI.Domain.Enums;

namespace EnglishTutorAI.Application.Models;

public record AddMessageToThreadModel(string ThreadId, string Content, ChatType ChatType);