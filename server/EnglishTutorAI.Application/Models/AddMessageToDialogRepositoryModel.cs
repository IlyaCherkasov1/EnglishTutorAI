using EnglishTutorAI.Domain.Enums;

namespace EnglishTutorAI.Application.Models;

public record AddMessageToDialogRepositoryModel(Guid UserTranslateId, string Content, ConversationRole Role);