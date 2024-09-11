using System.Linq.Expressions;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications.Configurations;

public interface IDataTransformSpecification<TSource, TResult> : ISpecification<TSource>
    where TSource : Entity
{
    Expression<Func<TSource, TResult>> Selector { get; }

    bool IsDistinct { get; }
}