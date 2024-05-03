using AutoMapper;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetStory;

public class GetStoryQueryHandler : IRequestHandler<GetStoryQuery, StoryResponse>
{
    private readonly IStoryRetrievalService _storyRetrievalService;
    private readonly IMapper _mapper;

    public GetStoryQueryHandler(IMapper mapper, IStoryRetrievalService storyRetrievalService)
    {
        _mapper = mapper;
        _storyRetrievalService = storyRetrievalService;
    }

    public async Task<StoryResponse> Handle(GetStoryQuery request, CancellationToken cancellationToken)
    {
        var story = await _storyRetrievalService.GetStoryByIndex(request.Index);

        return _mapper.Map<StoryResponse>(story);
    }
}