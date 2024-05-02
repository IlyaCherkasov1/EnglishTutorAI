using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

public class StoryCounterService : IStoryCounterService
{
    private readonly IRepository<Story> _storyRepository;

    public StoryCounterService(IRepository<Story> storyRepository)
    {
        _storyRepository = storyRepository;
    }

    public Task<int> Get()
    {
        return _storyRepository.Count();
    }
}