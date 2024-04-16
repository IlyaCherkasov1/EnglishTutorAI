using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

public class StoryService : IStoryService
{
    private readonly IRepository<Story> _storyRepository;

    public StoryService(IRepository<Story> storyRepository)
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

    public async Task AddStory(StoryCreationRequest creationRequest)
    {
        var story = new Story()
        {
            Title = creationRequest.Title,
            Content = creationRequest.Content,
            CreatedAt = DateTime.UtcNow,
        };

        await _storyRepository.Add(story);
    }
}