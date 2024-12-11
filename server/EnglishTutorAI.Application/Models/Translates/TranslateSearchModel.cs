using EnglishTutorAI.Application.Interfaces.common;
using EnglishTutorAI.Domain.Enums;

namespace EnglishTutorAI.Application.Models.Translates;

public class TranslateSearchModel : PaginationSearchModel, IPageable
{
    public StudyTopic StudyTopic { get; init; }
}