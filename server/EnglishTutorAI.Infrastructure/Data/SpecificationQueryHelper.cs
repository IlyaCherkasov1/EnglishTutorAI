using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnglishTutorAI.Infrastructure.Data;

internal static class SpecificationQueryHelper<T> where T : Entity
{
    public static IQueryable<T> BuildQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
    {
        var query = ApplyCriteria(inputQuery, specification);
        query = ApplyIncludes(query, specification);
        query = ApplyOrdering(query, specification);

        return query;
    }

    private static IQueryable<T> ApplyCriteria(IQueryable<T> query, ISpecification<T> specification)
    {
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        return query;
    }

    private static IQueryable<T> ApplyIncludes(IQueryable<T> query, ISpecification<T> specification)
    {
        query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

        return query;
    }

    private static IQueryable<T> ApplyOrdering(IQueryable<T> query, ISpecification<T> specification)
    {
        if (!specification.OrderByExpressions.Any())
        {
            return query;
        }

        IOrderedQueryable<T>? orderedQuery = null;
        foreach (var orderByExpression in specification.OrderByExpressions)
        {
            if (orderedQuery == null)
            {
                orderedQuery = orderByExpression.IsDescending
                    ? query.OrderByDescending(orderByExpression.KeySelector)
                    : query.OrderBy(orderByExpression.KeySelector);
            }
            else
            {
                orderedQuery = orderByExpression.IsDescending
                    ? orderedQuery.ThenByDescending(orderByExpression.KeySelector)
                    : orderedQuery.ThenBy(orderByExpression.KeySelector);
            }
        }

        query = orderedQuery ?? query;

        return query;
    }
}