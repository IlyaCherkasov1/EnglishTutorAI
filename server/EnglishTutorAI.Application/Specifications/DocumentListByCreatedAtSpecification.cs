using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public class DocumentListByCreatedAtSpecification : Specification<Document>
{
    public DocumentListByCreatedAtSpecification()
    {
        ApplyOrderByDescending(d => d.CreatedAt);
    }

}