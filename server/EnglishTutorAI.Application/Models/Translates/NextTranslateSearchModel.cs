using EnglishTutorAI.Domain.Enums;
using EnglishTutorAI.Domain.Interfaces;

namespace EnglishTutorAI.Application.Models.Translates;

public class NextTranslateSearchModel : IHasCreatedAt
{
    public StudyTopic StudyTopic { get; init; }

    public DateTime CreatedAt { get; set; }
}