using EnglishTutorAI.Domain.Enums;

namespace EnglishTutorAI.Application.Models;

public record AddMessageToDialogRepositoryModel(Guid UserDocumentId, string Content, ConversationRole Role);