using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Documents;
using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;
using EnglishTutorAI.Domain.Enums;

namespace EnglishTutorAI.Application.Specifications;

public class DocumentListSearchSpecification : DataTransformSpecification<Document, DocumentListItem>
{
    public DocumentListSearchSpecification(DocumentsSearchModel model, Guid userId) : base(
        d => new DocumentListItem
        {
            Id = d.Id,
            Title = d.Title,
            Content = string.Join(' ', d.Sentences.OrderBy(s => s.Position).Select(s => s.Text).Take(2)),
            StudyTopic = d.StudyTopic.ToString(),
            CreatedAt = d.CreatedAt,
        })
    {
        if (model.StudyTopic != StudyTopic.All)
        {
            ApplyCriteria(d => d.StudyTopic == model.StudyTopic);
        }

        AddInclude(d => d.Sentences);
        ApplyOrderByDescending(d => d.CreatedAt);
        ApplyPaging(model.PageNumber, model.PageSize);

        ApplyCriteria(d => d.UserDocuments
            .Where(ud => ud.UserId == userId && ud.DocumentId == d.Id)
            .Select(ud => ud.IsCompleted)
            .FirstOrDefault() == false);
    }
}