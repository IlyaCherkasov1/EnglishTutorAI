using System.Data;

namespace EnglishTutorAI.Application.Interfaces;

public interface IUnitOfWork
{
    Task Commit();

    IDbTransaction BeginTransaction();
}