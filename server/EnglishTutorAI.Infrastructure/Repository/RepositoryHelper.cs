using EnglishTutorAI.Application.Exceptions;
using EnglishTutorAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnglishTutorAI.Infrastructure.Repository;

public static class RepositoryHelper
{
    public static void SetEntityId<T>(T entity) where T : Entity
    {
        if (entity.Id == default)
        {
            entity.Id = Guid.NewGuid();
        }
    }

    public static async Task<T> GetSingleWithExceptionHandling<T>(IQueryable<T> queryable)
    {
        try
        {
            return await queryable.SingleAsync();
        }
        catch (InvalidOperationException ex)
        {
            throw new EntityNotFoundException("Expected exactly one entity, but found none or multiple.", ex);
        }
    }
}