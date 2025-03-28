﻿using EnglishTutorAI.Domain.Enums;
using EnglishTutorAI.Domain.Interfaces;

namespace EnglishTutorAI.Domain.Entities;

public class LinguaFixMessage : Entity, IHasCreatedAt
{
    public required string TranslatedText { get; init; }

    public required string CorrectedText { get; init; }

    public DateTime CreatedAt { get; set; }

    public required Guid SessionId { get; set; }

    public required Guid UserTranslateId { get; set; }

    public UserTranslate UserTranslate { get; init; } = null!;
}