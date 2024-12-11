using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Translates;
using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;
using EnglishTutorAI.Domain.Enums;

namespace EnglishTutorAI.Application.Specifications;

public class TranslateListSearchSpecification : DataTransformSpecification<Translate, TranslateListItem>
{
    public TranslateListSearchSpecification(TranslateSearchModel model, Guid userId) : base(
        d => new TranslateListItem
        {
            Id = d.Id,
            Title = d.Title,
            Content = string.Join(' ', d.Sentences.OrderBy(s => s.Position).Select(s => s.Text).Take(4)),
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

        ApplyCriteria(d => d.UserTranslates
            .Where(ud => ud.UserId == userId && ud.TranslateId == d.Id)
            .Select(ud => ud.IsCompleted)
            .FirstOrDefault() == false);
    }
}