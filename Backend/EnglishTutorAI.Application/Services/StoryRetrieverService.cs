using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

public class StoryRetrieverService : IStoryRetrieverService
{
    private readonly IRepository<Story> _storyRepository;

    public StoryRetrieverService(IRepository<Story> storyRepository)
    {
        _storyRepository = storyRepository;
    }

    public Task<Story> GetStory()
    {
        return _storyRepository.GetById(Guid.Parse("bd4b1c36-c6f3-4464-bc41-4591b7579b60"));
    }
}