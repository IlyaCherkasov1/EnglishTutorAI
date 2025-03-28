﻿using EnglishTutorAI.Domain.Enums;

namespace EnglishTutorAI.Application.Models;

public class TranslateCreationRequest
{
    public required string Title { get; set; }
    public required string Content { get; set; }

    public required StudyTopic StudyTopic { get; init; }
}