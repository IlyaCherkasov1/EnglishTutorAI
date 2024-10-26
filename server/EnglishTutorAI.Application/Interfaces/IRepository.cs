using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Interfaces;

public interface IRepository<T> where T : Entity
{
    Task<T> GetById(Guid id);

    Task<T?> GetSingleOrDefault(ISpecification<T> specification);

    Task<TResult?> GetSingleOrDefault<TResult>(IDataTransformSpecification<T, TResult> specification);

    Task<T?> GetFirstOrDefault(ISpecification<T> specification);

    Task<IReadOnlyList<T>> ListAll();

    Task<IReadOnlyList<T>> List(ISpecification<T> specification);

    Task<T> Add(T entity);

    Task Add(IEnumerable<T> entities);

    void Update(T entity);

    void Update(IEnumerable<T> entities);

    void Delete(T entity);

    Task Delete(ISpecification<T> specification);

    void DeleteIfExists(T? entity);

    Task DeleteById(Guid id);

    Task DeleteIfExists(ISpecification<T> specification);

    void DeleteRangeIfExists(ISpecification<T> specification);

    void DeleteRange(IEnumerable<T> entities);

    Task<SearchResult<T>> Search(ISpecification<T> specification);

    Task<SearchResult<TResult>> Search<TResult>(IDataTransformSpecification<T, TResult> specification);
}