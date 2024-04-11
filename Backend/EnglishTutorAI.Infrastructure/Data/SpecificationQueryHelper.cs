using System.Linq.Expressions;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Application.Specifications.ImplicitFilters;
using EnglishTutorAI.Domain.Entities;
using EnglishTutorAI.Domain.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EnglishTutorAI.Infrastructure.Data;

internal static class SpecificationQueryHelper<T>
    where T : Entity
{
    public static async Task<IQueryable<T>> BuildQuery(
        IQueryable<T> inputQuery,
        ISpecification<T> specification,
        IEnumerable<IDataFilter<T>> dataFilters)
    {
        var query = specification.IsReadOnly ? inputQuery.AsNoTracking() : inputQuery;
        query = await ApplyBaseQuery(query, specification, dataFilters);
        query = ApplyIncludes(query, specification);
        query = ApplyPaging(query, specification);

        return query;
    }

    public static async Task<IQueryable<TResult>> BuildDataTransformQuery<TResult>(
        IQueryable<T> inputQuery,
        IDataTransformSpecification<T, TResult> dataTransformSpecification,
        IEnumerable<IDataFilter<T>> dataFilters)
    {
        IQueryable<T> query = await BuildQuery(inputQuery, dataTransformSpecification, dataFilters);
        IQueryable<TResult> transformQuery = ApplySelector(query, dataTransformSpecification);

        return ApplyDistinct(transformQuery, dataTransformSpecification);
    }

    public static async Task<IQueryable<TResult>> BuildDataGroupQuery<TResult, TKey>(
        IQueryable<T> inputQuery,
        IDataGroupingSpecification<T, TKey, TResult> dataGroupingSpecification,
        IEnumerable<IDataFilter<T>> dataFilters)
    {
        IQueryable<T> query = await BuildQuery(inputQuery, dataGroupingSpecification, dataFilters);

        return ApplyGroup(query, dataGroupingSpecification);
    }

    public static Task<IQueryable<T>> BuildTotalCountQuery(
        IQueryable<T> inputQuery,
        ISpecification<T> specification,
        IEnumerable<IDataFilter<T>> dataFilters)
    {
        var query = inputQuery;
        return ApplyBaseQuery(query, specification, dataFilters);
    }

    private static async Task<IQueryable<T>> ApplyImplicitFilters(
        IQueryable<T> query,
        IEnumerable<IDataFilter<T>> dataFilters,
        IEnumerable<string> skippedFiltersKeys = null)
    {
        IEnumerable<IDataFilter<T>> filtersToApply;

        if (skippedFiltersKeys.IsNullOrEmpty())
        {
            filtersToApply = dataFilters;
        }
        else if (skippedFiltersKeys.Contains(FilterKeys.All))
        {
            return query;
        }
        else
        {
            filtersToApply = dataFilters.Where(df => !skippedFiltersKeys.Contains(df.FilterKey));
        }

        foreach (var dataFilter in filtersToApply)
        {
            Expression<Func<T, bool>> predicate = await dataFilter.GetPredicate();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
        }

        return query;
    }

    private static Task<IQueryable<T>> ApplyBaseQuery(
        IQueryable<T> query,
        ISpecification<T> specification,
        IEnumerable<IDataFilter<T>> dataFilters)
    {
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        return ApplyImplicitFilters(query, dataFilters, specification.SkippedImplicitFiltersKeys);
    }

    private static IQueryable<T> ApplyIncludes(IQueryable<T> query, ISpecification<T> specification)
    {
        query = specification.Includes.Aggregate(
            query,
            (current, include) => include.ThenInclude == null
                ? current.Include(include.Include)
                : current.Include(include.Include).ThenInclude(include.ThenInclude));

        query = specification.IncludeStrings.Aggregate(
            query,
            (current, include) => current.Include(include));

        return query;
    }

    private static IQueryable<TResult> ApplySelector<TResult>(
        IQueryable<T> query,
        IDataTransformSpecification<T, TResult> specification)
    {
        return query.Select(specification.Selector);
    }

    private static IQueryable<T> ApplyPaging(IQueryable<T> query, ISpecification<T> specification)
    {
        if (specification.IsPagingEnabled)
        {
            query = query.Skip(specification.Skip)
                .Take(specification.Take);
        }

        return query;
    }

    private static IQueryable<TResult> ApplyDistinct<TResult>(
        IQueryable<TResult> query,
        IDataTransformSpecification<T, TResult> specification)
    {
        if (specification.IsDistinct)
        {
            query = query.Distinct();
        }

        return query;
    }

    private static IQueryable<TResult> ApplyGroup<TResult, TKey>(
        IQueryable<T> query,
        IDataGroupingSpecification<T, TKey, TResult> dataGroupingSpecification)
    {
        return query.GroupBy(dataGroupingSpecification.GroupByExpression)
            .Select(dataGroupingSpecification.GroupTransformExpression);
    }
}