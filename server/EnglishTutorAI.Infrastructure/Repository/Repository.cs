using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnglishTutorAI.Infrastructure.Repository;

[ScopedDependency]
public class Repository<T> : IRepository<T> where T : Entity
{
    private readonly ISpecificationHandler<T> _specificationHandler;

    public Repository(ApplicationDbContext context, ISpecificationHandler<T> specificationHandler)
    {
        Context = context;
        _specificationHandler = specificationHandler;
    }

    private ApplicationDbContext Context { get; }

    public async Task<T> GetById(Guid id)
    {
        var queryable = _specificationHandler.ApplySpecification(new Specification<T>(e => e.Id == id));
        return await RepositoryHelper.GetSingleWithExceptionHandling(queryable);
    }

    public async Task<T?> GetSingleOrDefault(ISpecification<T> specification)
    {
        var queryable = _specificationHandler.ApplySpecification(specification);
        return await queryable.SingleOrDefaultAsync();
    }

    public async Task<TResult?> GetSingleOrDefault<TResult>(IDataTransformSpecification<T, TResult> specification)
    {
        var queryable = _specificationHandler.ApplyDataTransformSpecification(specification);
        return await queryable.SingleOrDefaultAsync();
    }

    public async Task<T?> GetFirstOrDefault(ISpecification<T> specification)
    {
        var queryable = _specificationHandler.ApplySpecification(specification);
        return await queryable.FirstOrDefaultAsync();
    }

    public async Task<TResult?> GetFirstOrDefault<TResult>(IDataTransformSpecification<T, TResult> specification)
    {
        var queryable = _specificationHandler.ApplyDataTransformSpecification(specification);
        return await queryable.FirstOrDefaultAsync();
    }

    public async Task<T> Single(ISpecification<T> specification)
    {
        var queryable = _specificationHandler.ApplySpecification(specification);
        return await queryable.SingleAsync();
    }

    public async Task<TResult> Single<TResult>(IDataTransformSpecification<T, TResult> specification)
    {
        var queryable = _specificationHandler.ApplyDataTransformSpecification(specification);
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
        var queryable = _specificationHandler.ApplyDataTransformSpecification(dataTransformSpecification);

        return await queryable.ToListAsync();
    }

    public async Task<T> Add(T entity)
    {
        RepositoryHelper.SetEntityId(entity);
        var result = await Context.AddAsync(entity);

        return result.Entity;
    }

    public async Task Add(IEnumerable<T> entities)
    {
        var entityList = entities.ToList();
        entityList.ForEach(RepositoryHelper.SetEntityId);

        await Context.AddRangeAsync(entityList);
    }

    public void Update(T entity)
    {
        Context.Set<T>().Update(entity);
    }

    public void Update(IEnumerable<T> entities)
    {
        Context.Set<T>().UpdateRange(entities);
    }

    public void Delete(T entity)
    {
        Context.Remove(entity);
    }

    public async Task Delete(ISpecification<T> specification)
    {
        Context.Remove(await _specificationHandler.ApplySpecification(specification).SingleAsync());
    }

    public void DeleteIfExists(T? entity)
    {
        if (entity != null)
        {
            Delete(entity);
        }
    }

    public async Task DeleteById(Guid id)
    {
        Context.Remove(await GetById(id));
    }

    public async Task DeleteIfExists(ISpecification<T> specification)
    {
        var entity = await _specificationHandler.ApplySpecification(specification).SingleOrDefaultAsync();

        if (entity != null)
        {
            Delete(entity);
        }
    }

    public void DeleteRangeIfExists(ISpecification<T> specification)
    {
        var entities = _specificationHandler.ApplySpecification(specification);

        if (entities.Any())
        {
            DeleteRange(entities);
        }
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        Context.RemoveRange(entities);
    }

    public async Task<SearchResult<T>> Search(ISpecification<T> specification)
    {
        var items = await _specificationHandler.ApplySpecification(specification).ToListAsync();
        var totalCount = await GetTotalCountWithoutPagingSpecification(specification);

        return new SearchResult<T>
        {
            Items = items,
            TotalCount = totalCount
        };
    }

    public async Task<SearchResult<TResult>> Search<TResult>(IDataTransformSpecification<T, TResult> specification)
    {
        var items = await _specificationHandler.ApplyDataTransformSpecification(specification).ToListAsync();
        var totalCount = await GetTotalCountWithoutPagingSpecification(specification);

        return new SearchResult<TResult>
        {
            Items = items,
            TotalCount = totalCount,
        };
    }

    public async Task<bool> Any(ISpecification<T> specification)
    {
        var queryable = _specificationHandler.ApplySpecification(specification);

        return await queryable.AnyAsync();
    }

    public async Task<bool> Any()
    {
        var queryable = _specificationHandler.ApplySpecification(new Specification<T>());

        return await queryable.AnyAsync();
    }


    private async Task<IReadOnlyList<T>> ListInternal(ISpecification<T> specification)
    {
        var queryable = _specificationHandler.ApplySpecification(specification);

        return await queryable.ToListAsync();
    }

    private async Task<int> GetTotalCountWithoutPagingSpecification(ISpecification<T> spec)
    {
        return await SpecificationQueryHelper<T>.ApplyCriteria(Context.Set<T>().AsQueryable(), spec).CountAsync();
    }
}