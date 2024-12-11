using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public class CompletedUserTranslatesSpecification : DataTransformSpecification<Translate, CompletedTranslateListItem>
{
    public CompletedUserTranslatesSpecification(Guid userId, PaginationSearchModel model) : base(
        d => new CompletedTranslateListItem
        {
            Title = d.Title,
            Content = string.Join(' ', d.Sentences.OrderBy(s => s.Position).Select(s => s.Text)),
            StudyTopic = d.StudyTopic,
            TranslateId = d.Id,
            CompletedOn = d.UserTranslates.Where(ud => ud.UserId == userId && ud.TranslateId == d.Id)
                .Select(ud => ud.CompletedOn)
                .First()
        },
        d => d.UserTranslates.Any(ud => ud.UserId == userId && ud.IsCompleted))
    {
        ApplyOrderByDescending(d => d.CreatedAt);
        ApplyPaging(model.PageNumber, model.PageSize);
    }
}