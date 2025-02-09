﻿using EnglishTutorAI.Domain.Interfaces;

namespace EnglishTutorAI.Application.Models.Translates;

public class MistakeHistoryItems : IHasCreatedAt
{
    public Guid Id { get; set; }
    public required string TranslatedText { get; set; }

    public required string CorrectedText { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid TranslateId { get; set; }
}