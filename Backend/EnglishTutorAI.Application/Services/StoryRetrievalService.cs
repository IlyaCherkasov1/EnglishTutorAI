using AutoMapper;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

public class StoryRetrievalService : IStoryRetrievalService
{
    private readonly IRepository<Story> _storyRepository;
    private readonly IMapper _mapper;

    public StoryRetrievalService(IRepository<Story> storyRepository, IMapper mapper)
    {
        _storyRepository = storyRepository;
        _mapper = mapper;
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

    public async Task<IReadOnlyList<StoryListItem>> GetAllStories()
    {
        return _mapper.Map<IReadOnlyList<StoryListItem>>(await _storyRepository.ListAll());
    }
}