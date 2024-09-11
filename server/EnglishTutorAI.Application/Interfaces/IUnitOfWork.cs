namespace EnglishTutorAI.Application.Interfaces;

public interface IUnitOfWork
{
    Task Commit();
}