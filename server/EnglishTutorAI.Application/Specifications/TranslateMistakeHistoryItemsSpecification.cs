using System.Linq.Expressions;
using EnglishTutorAI.Application.Models.Translates;
using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public class TranslateMistakeHistoryItemsSpecification : DataTransformSpecification<LinguaFixMessage, TranslateMistakeHistoryItems>
{
    public TranslateMistakeHistoryItemsSpecification(Guid sessionId) : base(
        m => new TranslateMistakeHistoryItems
        {
            Id = m.Id,
            CorrectedText = m.CorrectedText,
            TranslatedText = m.TranslatedText,
        }, m => m.SessionId == sessionId)
    {
        ApplyOrderBy(m => m.CreatedAt);
    }
}