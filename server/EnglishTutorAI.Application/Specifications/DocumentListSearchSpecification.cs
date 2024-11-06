using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Documents;
using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;
using EnglishTutorAI.Domain.Enums;

namespace EnglishTutorAI.Application.Specifications;

public class DocumentListSearchSpecification : DataTransformSpecification<Document, DocumentListItem>
{
    public DocumentListSearchSpecification(DocumentsSearchModel model) : base(
        d => new DocumentListItem
        {
            Id = d.Id,
            Title = d.Title,
            Content = d.Content,
            StudyTopic = d.StudyTopic.ToString(),
            CreatedAt = d.CreatedAt,
        })
    {
        if (model.StudyTopic != StudyTopic.All)
        {
            ApplyCriteria(d => d.StudyTopic == model.StudyTopic);
        }

        ApplyOrderByDescending(d => d.CreatedAt);
        ApplyPaging(model.PageNumber, model.PageSize);
    }
}