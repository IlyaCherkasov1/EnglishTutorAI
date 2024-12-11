namespace EnglishTutorAI.Application.Interfaces;

public interface ITranslateDeletionService
{
    Task Delete(Guid translateId);
}