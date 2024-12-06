namespace EnglishTutorAI.Application.Models;

public record SaveStatisticsAndMessageModel(string TranslatedText, string CorrectedText, Guid UserDocumentId);