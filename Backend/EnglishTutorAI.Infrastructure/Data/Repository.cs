using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Application.Specifications.ImplicitFilters;
using EnglishTutorAI.Domain.Entities;
using EnglishTutorAI.Domain.Exceptions;
using EnglishTutorAI.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EnglishTutorAI.Infrastructure.Data;

public class Repository<T> : IRepository<T>
    where T : Entity
{
    public Repository(
        ApplicationDbContext context,
        IEnumerable<IDataFilter<T>> dataFilters)
    {
        Context = context;
        DataFilters = dataFilters;
    }

    protected ApplicationDbContext Context { get; }

    protected IEnumerable<IDataFilter<T>> DataFilters { get; }

    public async Task<T> GetSingleOrDefault(ISpecification<T> specification)
    {
        IQueryable<T> queryable = await ApplySpecification(specification);

        return await GetSingleOrDefault(queryable, specification.TreatEmptyResultAsConcurrency);
    }

    public async Task<TResult> GetSingleOrDefault<TResult>(IDataTransformSpecification<T, TResult> specification)
    {
        IQueryable<TResult> queryable = await ApplyDataTransformSpecification(specification);

        return await GetSingleOrDefault(queryable, specification.TreatEmptyResultAsConcurrency);
    }

    public async Task<T> GetFirstOrDefault(ISpecification<T> specification)
    {
        IQueryable<T> queryable = await ApplySpecification(specification);

        return await GetFirstOrDefault(queryable, specification.TreatEmptyResultAsConcurrency);
    }

    public async Task<TResult> GetFirstOrDefault<TResult>(IDataTransformSpecification<T, TResult> specification)
    {
        IQueryable<TResult> queryable = await ApplyDataTransformSpecification(specification);

        return await GetFirstOrDefault(queryable, specification.TreatEmptyResultAsConcurrency);
    }

    public async Task<T> GetById(Guid id)
    {
        IQueryable<T> queryable = await ApplySpecification(new Specification<T>(e => e.Id == id));

        return await queryable.SingleAsync();
    }

    public Task<IReadOnlyList<T>> ListAll()
    {
        return ListInternal(new Specification<T>());
    }

    public Task<IReadOnlyList<T>> List(ISpecification<T> specification)
    {
        return ListInternal(specification);
    }

    public async Task<IReadOnlyList<TResult>> List<TResult>(
        IDataTransformSpecification<T, TResult> dataTransformSpecification)
    {
        IQueryable<TResult> queryable = await ApplyDataTransformSpecification(dataTransformSpecification);

        return await queryable.ToListAsync();
    }

    public async Task<IReadOnlyList<TResult>> List<TKey, TResult>(
        IDataGroupingSpecification<T, TKey, TResult> dataGroupingSpecification)
    {
        IQueryable<TResult> queryable = await ApplyGroupSpecification(dataGroupingSpecification);

        return await queryable.ToListAsync();
    }

    public async Task<int> Count(ISpecification<T> specification)
    {
        IQueryable<T> queryable = await ApplySpecification(specification);

        return await queryable.CountAsync();
    }

    public async Task<int> Count()
    {
        IQueryable<T> queryable = await ApplySpecification(new Specification<T>());

        return await queryable.CountAsync();
    }

    public async Task<T> Add(T entity)
    {
        SetEntityId(entity);
        EntityEntry<T> result = await Context.AddAsync(entity);

        return result.Entity;
    }

    public Task Add(T[] entities)
    {
        Array.ForEach(entities, SetEntityId);

        return Context.AddRangeAsync(entities);
    }

    public Task<T> Update(T entity)
    {
        EntityEntry<T> result = Context.Set<T>().Update(entity);

        return Task.FromResult(result.Entity);
    }

    public Task Update(IEnumerable<T> entities)
    {
        Context.Set<T>().UpdateRange(entities);

        return Task.CompletedTask;
    }

    public async Task DeleteById(Guid id)
    {
        Context.Remove(await GetById(id));
    }

    public Task Delete(T entity)
    {
        Context.Remove(entity);

        return Task.CompletedTask;
    }

    public Task DeleteIfExists(T entity)
    {
        return entity == null ? Task.CompletedTask : Delete(entity);
    }

    public async Task DeleteIfExists(ISpecification<T> specification)
    {
        var entities = await ApplySpecification(specification);

        if (!entities.IsNullOrEmpty())
        {
            await DeleteRange(entities);
        }
    }

    public async Task DeleteSingleIfExists(ISpecification<T> specification)
    {
        var entity = await (await ApplySpecification(specification)).SingleOrDefaultAsync();

        if (entity != null)
        {
            await Delete(entity);
        }
    }

    public async Task DeleteSingle(ISpecification<T> specification)
    {
        Context.Remove(await (await ApplySpecification(specification)).SingleAsync());
    }

    public Task DeleteRange(IEnumerable<T> entities)
    {
        Context.RemoveRange(entities);

        return Task.CompletedTask;
    }

    public async Task DeleteRange(ISpecification<T> specification)
    {
        Context.RemoveRange(await ApplySpecification(specification));
    }

    public Task DeleteAll()
    {
        var context = Context.Set<T>();
        context.RemoveRange(context.AsQueryable());

        return Task.CompletedTask;
    }

    public async Task<bool> Any(ISpecification<T> specification)
    {
        IQueryable<T> queryable = await ApplySpecification(specification);

        return await queryable.AnyAsync();
    }

    public async Task<bool> Any()
    {
        IQueryable<T> queryable = await ApplySpecification(new Specification<T>());

        return await queryable.AnyAsync();
    }

    private static async Task<TSource> GetSingleOrDefault<TSource>(
        IQueryable<TSource> queryable, bool treatEmptyResultAsConcurrency)
    {
        return (await GetEntitiesWithEmptyResultHandling(queryable, treatEmptyResultAsConcurrency)).SingleOrDefault();
    }

    private static async Task<TSource> GetFirstOrDefault<TSource>(
        IQueryable<TSource> queryable, bool treatEmptyResultAsConcurrency)
    {
        return (await GetEntitiesWithEmptyResultHandling(queryable, treatEmptyResultAsConcurrency)).FirstOrDefault();
    }

    private static async Task<List<TSource>> GetEntitiesWithEmptyResultHandling<TSource>(
        IQueryable<TSource> queryable, bool treatEmptyResultAsConcurrency)
    {
        var entities = await queryable.ToListAsync();
        if (treatEmptyResultAsConcurrency && entities.Count == 0)
        {
            throw new PotentiallyConcurrentModificationsException($"The entity of type {typeof(T)} was not found");
        }

        return entities;
    }

    private async Task<IReadOnlyList<T>> ListInternal(ISpecification<T> specification)
    {
        IQueryable<T> queryable = await ApplySpecification(specification);

        return await queryable.ToListAsync();
    }

    private void SetEntityId(T entity)
    {
        if (entity.Id != default)
        {
            return;
        }

        entity.Id = Guid.NewGuid();
    }

    private Task<IQueryable<T>> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationQueryHelper<T>.BuildQuery(Context.Set<T>().AsQueryable(), spec, DataFilters);
    }

    private Task<IQueryable<T>> ApplySpecificationForTotalCount(ISpecification<T> spec)
    {
        return SpecificationQueryHelper<T>.BuildTotalCountQuery(Context.Set<T>().AsQueryable(), spec, DataFilters);
    }

    private Task<IQueryable<TResult>> ApplyDataTransformSpecification<TResult>(
        IDataTransformSpecification<T, TResult> dataTransformSpecification)
    {
        return SpecificationQueryHelper<T>.BuildDataTransformQuery(
            Context.Set<T>().AsQueryable(),
            dataTransformSpecification,
            DataFilters);
    }

    private Task<IQueryable<TResult>> ApplyGroupSpecification<TResult, TKey>(
        IDataGroupingSpecification<T, TKey, TResult> dataGroupingSpecification)
    {
        return SpecificationQueryHelper<T>.BuildDataGroupQuery(
            Context.Set<T>().AsQueryable(),
            dataGroupingSpecification,
            DataFilters);
    }
}