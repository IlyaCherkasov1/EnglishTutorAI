namespace EnglishTutorAI.Application.Interfaces;

public interface IDocumentStartAgainService
{
    Task StartAgain(Guid userDocumentId);
}