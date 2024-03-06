using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Interfaces;

public interface IRepository<T> where T : Entity
{
    Task<T> GetSingleOrDefault(ISpecification<T> specification);

    Task<T> GetById(Guid id);

    Task<IReadOnlyList<T>> ListAll();

    Task<IReadOnlyList<T>> List(ISpecification<T> specification);

    Task<T> Add(T entity);

    Task Add(T[] entities);

    Task<T> Update(T entity);

    Task Update(IEnumerable<T> entities);

    Task DeleteById(Guid id);

    Task Delete(T entity);
}