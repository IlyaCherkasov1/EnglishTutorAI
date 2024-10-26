using EnglishTutorAI.Application.Interfaces.common;
using EnglishTutorAI.Domain.Enums;

namespace EnglishTutorAI.Application.Models.Documents;

public class DocumentsSearchModel : IPageable
{
    public StudyTopic StudyTopic { get; init; }

    public int PageNumber { get; init; }

    public int PageSize { get; init; }
}