using System.Linq.Expressions;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public interface ISpecification<T>
    where T : Entity
{
    List<string> SkippedImplicitFiltersKeys { get; }

    Expression<Func<T, bool>> Criteria { get; }

    List<Expression<Func<T, bool>>> OutOfPagingCriteria { get; }

    List<IncludeModel<T, object>> Includes { get; }

    List<string> IncludeStrings { get; }

    int Take { get; }

    int Skip { get; }

    bool IsPagingEnabled { get; }

    bool IsReadOnly { get; }

    bool TreatEmptyResultAsConcurrency { get; }
}