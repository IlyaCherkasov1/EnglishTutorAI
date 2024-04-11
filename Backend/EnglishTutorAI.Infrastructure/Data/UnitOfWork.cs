using EnglishTutorAI.Application.Interfaces;

namespace EnglishTutorAI.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task Commit()
    {
        return _context.SaveChangesAsync();
    }
}