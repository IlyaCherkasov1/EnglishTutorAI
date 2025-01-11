using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Interfaces;

public interface ISpecificationHandler<T> where T : Entity
{
    IQueryable<T> ApplySpecification(ISpecification<T> spec);

    IQueryable<TResult> ApplyDataTransformSpecification<TResult>(
        IDataTransformSpecification<T, TResult> dataTransformSpecification);
}