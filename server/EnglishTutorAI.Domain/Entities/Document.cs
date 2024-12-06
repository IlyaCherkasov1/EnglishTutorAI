using EnglishTutorAI.Domain.Enums;
using EnglishTutorAI.Domain.Interfaces;

namespace EnglishTutorAI.Domain.Entities;

public class Document : Entity, IHasCreatedAt
{
    public required string Title { get; init; }

    public DateTime CreatedAt { get; set; }

    public required StudyTopic StudyTopic { get; init; }

    public ICollection<DocumentSentence> Sentences { get; set; } = null!;

    public ICollection<UserDocument> UserDocuments { get; set; } = null!;
}