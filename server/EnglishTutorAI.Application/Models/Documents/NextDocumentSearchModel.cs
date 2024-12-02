using EnglishTutorAI.Domain.Enums;
using EnglishTutorAI.Domain.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EnglishTutorAI.Application.Models.Documents;

public class NextDocumentSearchModel : IHasCreatedAt
{
    public StudyTopic StudyTopic { get; init; }

    public DateTime CreatedAt { get; set; }
}