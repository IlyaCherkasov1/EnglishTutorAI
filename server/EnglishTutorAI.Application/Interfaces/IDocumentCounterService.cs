namespace EnglishTutorAI.Application.Interfaces;

public interface IDocumentCounterService
{
    Task<int> Get();
}