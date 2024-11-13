using System.Linq.Expressions;
using EnglishTutorAI.Application.Models.Documents;
using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public class DocumentMistakeHistoryItemsSpecification : DataTransformSpecification<LinguaFixMessage, DocumentMistakeHistoryItems>
{
    public DocumentMistakeHistoryItemsSpecification(Guid sessionId) : base(
        m => new DocumentMistakeHistoryItems
        {
            Id = m.Id,
            CorrectedText = m.CorrectedText,
            TranslatedText = m.TranslatedText,
        }, m => m.DocumentSessionId == sessionId)
    {
        ApplyOrderBy(m => m.CreatedAt);
    }
}