using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public class StoryByIndexSpecification : Specification<Story>
{
    public StoryByIndexSpecification()
    {
        ApplyOrderBy(s => s.CreatedAt);
    }
}