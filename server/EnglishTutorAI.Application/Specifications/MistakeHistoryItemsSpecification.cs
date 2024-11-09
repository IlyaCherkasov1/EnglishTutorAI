using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Documents;
using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public class MistakeHistoryItemsSpecification : DataTransformSpecification<LinguaFixMessage, MistakeHistoryItems>
{
    public MistakeHistoryItemsSpecification(PaginationSearchModel model) : base(
        m => new MistakeHistoryItems
        {
            Id = m.Id,
            CorrectedText = m.CorrectedText,
            TranslatedText = m.TranslatedText,
            CreatedAt = m.CreatedAt,
        },
        m => m.TranslatedText != m.CorrectedText)
    {
        ApplyOrderByDescending(d => d.CreatedAt);
        ApplyPaging(model.PageNumber, model.PageSize);
    }
}