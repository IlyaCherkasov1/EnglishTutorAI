using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public class CompletedUserDocumentsSpecification : DataTransformSpecification<Document, CompletedDocumentListItem>
{
    public CompletedUserDocumentsSpecification(Guid userId, PaginationSearchModel model) : base(
        d => new CompletedDocumentListItem
        {
            Title = d.Title,
            Content = string.Join(' ', d.Sentences.OrderBy(s => s.Position).Select(s => s.Text)),
            StudyTopic = d.StudyTopic,
            DocumentId = d.Id,
            CompletedOn = d.UserDocuments.Where(ud => ud.UserId == userId && ud.DocumentId == d.Id)
                .Select(ud => ud.CompletedOn)
                .FirstOrDefault()
        },
        d => d.UserDocuments.Any(ud => ud.UserId == userId && ud.IsCompleted))
    {
        ApplyOrderByDescending(d => d.CreatedAt);
        ApplyPaging(model.PageNumber, model.PageSize);
    }
}