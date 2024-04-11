namespace EnglishTutorAI.Application.Interfaces;

public interface IStoryCounterService
{
    Task<int> Get();
}