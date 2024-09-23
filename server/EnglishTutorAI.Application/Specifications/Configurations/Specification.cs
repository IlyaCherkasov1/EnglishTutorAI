using System.Linq.Expressions;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications.Configurations;

public class Specification<T> : ISpecification<T>
    where T : Entity
{
    public Specification()
    {
    }

    public Specification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<T, bool>>? Criteria { get; private set; }

    public List<Expression<Func<T, object>>> Includes { get; } = new();

    public List<(Expression<Func<T, object>> KeySelector, bool IsDescending)> OrderByExpressions { get; } = new();

    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderByExpressions.Add((orderByExpression, false));
    }

    protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderByExpressions.Add((orderByDescExpression, true));
    }
}