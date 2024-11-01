using EnglishTutorAI.Application.Interfaces.common;
using EnglishTutorAI.Domain.Enums;

namespace EnglishTutorAI.Application.Models.Documents;

public class DocumentsSearchModel : PaginationSearchModel, IPageable
{
    public StudyTopic StudyTopic { get; init; }
}