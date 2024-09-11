using System.Linq.Expressions;

namespace EnglishTutorAI.Application.Specifications.ImplicitFilters;

public interface IDataFilter<T>
{
    string FilterKey { get; }

    Task<Expression<Func<T, bool>>> GetPredicate();
}