using AutoMapper;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetStories;

public class GetStoryQueryHandler : IRequestHandler<GetStoryQuery, StoryResponse>
{
    private readonly IStoryRetrieverService _storyRetrieverService;
    private readonly IMapper _mapper;

    public GetStoryQueryHandler(IStoryRetrieverService storyRetrieverService, IMapper mapper)
    {
        _storyRetrieverService = storyRetrieverService;
        _mapper = mapper;
    }

    public async Task<StoryResponse> Handle(GetStoryQuery request, CancellationToken cancellationToken)
    {
        var story = await _storyRetrieverService.GetStory();

        return _mapper.Map<StoryResponse>(story);
    }
}