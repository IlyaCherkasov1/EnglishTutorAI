using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnglishTutorAI.Infrastructure.Data;

[ScopedDependency]
public class Repository<T> : IRepository<T> where T : Entity
{
    public Repository(ApplicationDbContext context)
    {
        Context = context;
    }

    private ApplicationDbContext Context { get; }

    public async Task<T> GetById(Guid id)
    {
        var queryable = ApplySpecification(new Specification<T>(e => e.Id == id));

        return await queryable.SingleAsync();
    }

    public async Task<T?> GetSingleOrDefault(ISpecification<T> specification)
    {
        var queryable = ApplySpecification(specification);
        var entities = await queryable.ToListAsync();

        return entities.SingleOrDefault();
    }

    public async Task<T?> GetFirstOrDefault(ISpecification<T> specification)
    {
        var queryable = ApplySpecification(specification);
        var entities = await queryable.ToListAsync();

        return entities.FirstOrDefault();
    }

    public Task<IReadOnlyList<T>> ListAll()
    {
        return ListInternal(new Specification<T>());
    }

    public Task<IReadOnlyList<T>> List(ISpecification<T> specification)
    {
        return ListInternal(specification);
    }

    public async Task<T> Add(T entity)
    {
        SetEntityId(entity);
        var result = await Context.AddAsync(entity);

        return result.Entity;
    }

    public async Task Add(IEnumerable<T> entities)
    {
        var entityList = entities.ToList();
        entityList.ForEach(SetEntityId);

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
        Context.Remove(await ApplySpecification(specification).SingleAsync());
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
        var entity = await ApplySpecification(specification).SingleOrDefaultAsync();

        if (entity != null)
        {
            Delete(entity);
        }
    }

    public void DeleteRangeIfExists(ISpecification<T> specification)
    {
        var entities = ApplySpecification(specification);

        if (entities.Any())
        {
            DeleteRange(entities);
        }
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        Context.RemoveRange(entities);
    }

    private async Task<IReadOnlyList<T>> ListInternal(ISpecification<T> specification)
    {
        var queryable = ApplySpecification(specification);

        return await queryable.ToListAsync();
    }

    private static void SetEntityId(T entity)
    {
        if (entity.Id != default)
        {
            return;
        }

        entity.Id = Guid.NewGuid();
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationQueryHelper<T>.BuildQuery(Context.Set<T>().AsQueryable(), spec);
    }
}