using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

public class StoryRetrieverService : IStoryRetrieverService
{
    private readonly IRepository<Story> _storyRepository;

    public StoryRetrieverService(IRepository<Story> storyRepository)
    {
        _storyRepository = storyRepository;
    }

    public async Task<Story> GetStoryByIndex(int index)
    {
        var storyCount = await _storyRepository.Count();

        if (index >= 0 && index < storyCount)
        {
            return (await _storyRepository.GetByIndex(index, new StoryByIndexSpecification()))!;
        }

        throw new IndexOutOfRangeException("Index is out of range.");
    }
}