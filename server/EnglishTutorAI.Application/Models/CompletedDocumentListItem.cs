using EnglishTutorAI.Domain.Enums;

namespace EnglishTutorAI.Application.Models;

public class CompletedDocumentListItem
{
    public Guid DocumentId { get; set; }

    public required string Title { get; set; }

    public StudyTopic StudyTopic { get; set; }

    public required string Content { get; set; }

    public DateTime CompletedOn { get; set; }
}