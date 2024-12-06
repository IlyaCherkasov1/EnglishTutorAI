using EnglishTutorAI.Application.Models.Documents;

namespace EnglishTutorAI.Application.Interfaces;

public interface IDocumentMistakeHistoryService
{
    Task<IEnumerable<DocumentMistakeHistoryItems>> Get(Guid userDocumentId);
}