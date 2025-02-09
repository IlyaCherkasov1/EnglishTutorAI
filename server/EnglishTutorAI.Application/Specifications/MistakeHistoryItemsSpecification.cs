﻿using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Translates;
using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public class MistakeHistoryItemsSpecification : DataTransformSpecification<LinguaFixMessage, MistakeHistoryItems>
{
    public MistakeHistoryItemsSpecification(PaginationSearchModel model, Guid userId) : base(
        m => new MistakeHistoryItems
        {
            Id = m.Id,
            CorrectedText = m.CorrectedText,
            TranslatedText = m.TranslatedText,
            CreatedAt = m.CreatedAt,
            TranslateId = m.UserTranslate.TranslateId,
        },
        m => m.TranslatedText != m.CorrectedText && m.UserTranslate.UserId == userId)
    {
        ApplyOrderByDescending(d => d.CreatedAt);
        ApplyPaging(model.PageNumber, model.PageSize);
    }
}