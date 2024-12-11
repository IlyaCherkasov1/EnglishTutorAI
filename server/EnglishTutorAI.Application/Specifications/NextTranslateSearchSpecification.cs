using System.Linq.Expressions;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Translates;
using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;
using EnglishTutorAI.Domain.Enums;

namespace EnglishTutorAI.Application.Specifications;

public class NextTranslateSearchSpecification : DataTransformSpecification<Translate, TranslateListItem>
{
    public NextTranslateSearchSpecification(NextTranslateSearchModel model, Guid userId) : base(
        d => new TranslateListItem
        {
            Id = d.Id,
            Title = d.Title,
            Content = string.Join(' ', d.Sentences.OrderBy(s => s.Position).Select(s => s.Text).Take(2)),
            StudyTopic = d.StudyTopic.ToString(),
            CreatedAt = d.CreatedAt
        }, d => d.CreatedAt < model.CreatedAt)
    {
        if (model.StudyTopic != StudyTopic.All)
        {
            ApplyCriteria(d => d.StudyTopic == model.StudyTopic);
        }

        AddInclude(d => d.Sentences);
        ApplyOrderByDescending(d => d.CreatedAt);

        ApplyCriteria(d => d.UserTranslates
            .Where(ud => ud.UserId == userId && ud.TranslateId == d.Id)
            .Select(ud => ud.IsCompleted)
            .FirstOrDefault() == false);
    }
}