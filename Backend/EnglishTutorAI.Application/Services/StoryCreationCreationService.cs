using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

public class StoryCreationCreationService : IStoryCreationService
{
    private readonly IRepository<Story> _storyRepository;

    public StoryCreationCreationService(IRepository<Story> storyRepository)
    {
        _storyRepository = storyRepository;
    }

    public async Task AddStory(StoryCreationRequest creationRequest)
    {
        var story = new Story()
        {
            Title = creationRequest.Title,
            Content = creationRequest.Content,
        };

        await _storyRepository.Add(story);
    }
}