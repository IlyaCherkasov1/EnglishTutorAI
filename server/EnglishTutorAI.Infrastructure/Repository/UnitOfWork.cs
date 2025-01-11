using System.Data;
using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace EnglishTutorAI.Infrastructure.Repository;

[ScopedDependency]
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    private static readonly Type[] CommonInterfaces =
    {
        typeof(IHasCreatedAt),
        typeof(IHasUpdatedAt),
    };

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task Commit()
    {
        SetCommonProperties();
        return _context.SaveChangesAsync();
    }

    public IDbTransaction BeginTransaction()
    {
        var transaction = _context.Database.BeginTransaction();

        return transaction.GetDbTransaction();
    }

    private void SetCommonProperties()
    {
        foreach (var entity in GetAddedAndUpdatedEntities())
        {
            SetEntityOperationDateProperties(entity);
        }
    }

    private static void SetEntityOperationDateProperties(EntityEntry entity)
    {
        switch (entity.Entity)
        {
            case IHasCreatedAt createdEntity when entity.State == EntityState.Added:
                createdEntity.CreatedAt = DateTime.UtcNow;
                break;
            case IHasUpdatedAt updatedEntity when entity.State == EntityState.Modified:
                updatedEntity.UpdatedAt = DateTime.UtcNow;
                break;
        }
    }

    private IEnumerable<EntityEntry> GetAddedAndUpdatedEntities() => _context.ChangeTracker.Entries().Where(x =>
        x.Entity.GetType().GetInterfaces().Intersect(CommonInterfaces).Any() &&
        x.State is EntityState.Added or EntityState.Modified);
}