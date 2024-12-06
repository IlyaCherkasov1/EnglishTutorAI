namespace EnglishTutorAI.Application.Interfaces;

public interface IDocumentCompletionService
{
    Task Save(Guid userDocumentId);
}