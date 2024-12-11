using EnglishTutorAI.Domain.Enums;

namespace EnglishTutorAI.Application.Models;

public class CompletedTranslateListItem
{
    public Guid TranslateId { get; set; }

    public required string Title { get; set; }

    public StudyTopic StudyTopic { get; set; }

    public required string Content { get; set; }

    public DateTime? CompletedOn { get; set; }
}