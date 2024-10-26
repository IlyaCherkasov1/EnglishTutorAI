using System.Linq.Expressions;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications.Configurations;

public class Specification<T> : ISpecification<T>
    where T : Entity
{
    private const int MaxPageSize = 100;

    public Specification()
    {
    }

    public Specification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<T, bool>>? Criteria { get; private set; }

    public List<Expression<Func<T, object>>> Includes { get; } = new();

    public int Take { get; private set; }

    public List<(Expression<Func<T, object>> KeySelector, bool IsDescending)> OrderByExpressions { get; } = new();

    public int Skip { get; private set; }

    public bool IsPagingEnabled { get; private set; }

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

    protected void ApplyPaging(int pageNumber, int pageSize)
    {
        if (pageSize > MaxPageSize)
        {
            throw new ArgumentOutOfRangeException(
                nameof(pageSize),
                $"Size of requested page ({pageSize}) exceeds maximum value of {MaxPageSize} items");
        }

        Skip = (pageNumber - 1) * pageSize;
        Take = pageSize;
        IsPagingEnabled = true;
    }

    protected void ApplyCriteria(Expression<Func<T, bool>> newCriteria)
    {
        if (Criteria == null)
        {
            Criteria = newCriteria;
        }
        else
        {
            Criteria = Criteria.AndAlso(newCriteria);
        }
    }
}