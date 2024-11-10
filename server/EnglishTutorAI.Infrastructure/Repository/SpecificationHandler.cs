using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Infrastructure.Repository;

[ScopedDependency]
public class SpecificationHandler<T> : ISpecificationHandler<T> where T : Entity
{
    private readonly ApplicationDbContext _context;

    public SpecificationHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationQueryHelper<T>.BuildQuery(_context.Set<T>().AsQueryable(), spec);
    }

    public IQueryable<TResult> ApplyDataTransformSpecification<TResult>(
        IDataTransformSpecification<T, TResult> dataTransformSpec)
    {
        return SpecificationQueryHelper<T>.BuildDataTransformQuery(_context.Set<T>().AsQueryable(), dataTransformSpec);
    }
}