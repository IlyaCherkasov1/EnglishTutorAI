using AutoMapper;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetStories;

public class GetStoryQueryHandler : IRequestHandler<GetStoryQuery, StoryResponse>
{
    private readonly IStoryService _storyService;
    private readonly IMapper _mapper;

    public GetStoryQueryHandler(IStoryService storyRetrieverService, IMapper mapper)
    {
        _storyService = storyRetrieverService;
        _mapper = mapper;
    }

    public async Task<StoryResponse> Handle(GetStoryQuery request, CancellationToken cancellationToken)
    {
        var story = await _storyService.GetStoryByIndex(request.Index);

        return _mapper.Map<StoryResponse>(story);
    }
}