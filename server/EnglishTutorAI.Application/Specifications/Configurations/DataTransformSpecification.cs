using System.Linq.Expressions;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications.Configurations;

public class DataTransformSpecification<TSource, TResult>
    : Specification<TSource>, IDataTransformSpecification<TSource, TResult>
    where TSource : Entity
{
    public DataTransformSpecification(
        Expression<Func<TSource, TResult>> selector,
        Expression<Func<TSource, bool>> criteria = null)
        : base(criteria)
    {
        Selector = selector ?? throw new ArgumentNullException(nameof(selector));
    }

    public Expression<Func<TSource, TResult>> Selector { get; }
}