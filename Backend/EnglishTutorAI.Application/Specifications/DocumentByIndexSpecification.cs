using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public class DocumentByIndexSpecification : Specification<Document>
{
    public DocumentByIndexSpecification()
    {
        ApplyOrderBy(s => s.CreatedAt);
    }
}