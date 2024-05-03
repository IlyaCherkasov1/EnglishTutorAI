using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetStories;

public class GetStoriesQueryHandler : IRequestHandler<GetStoriesQuery, IReadOnlyList<StoryListItem>>
{
    private readonly IStoryRetrievalService _storyRetrievalService;

    public GetStoriesQueryHandler(IStoryRetrievalService storyRetrievalService)
    {
        _storyRetrievalService = storyRetrievalService;
    }

    public Task<IReadOnlyList<StoryListItem>> Handle(GetStoriesQuery request, CancellationToken cancellationToken)
    {
        return _storyRetrievalService.GetAllStories();
    }
}