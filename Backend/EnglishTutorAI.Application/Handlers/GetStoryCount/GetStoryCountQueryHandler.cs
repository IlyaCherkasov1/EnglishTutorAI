using EnglishTutorAI.Application.Interfaces;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetStoryCount;

public class GetStoryCountQueryHandler : IRequestHandler<GetStoryCountQuery, int>
{
    private readonly IStoryCounterService _storyCounterService;

    public GetStoryCountQueryHandler(IStoryCounterService storyCounterService)
    {
        _storyCounterService = storyCounterService;
    }

    public Task<int> Handle(GetStoryCountQuery request, CancellationToken cancellationToken)
    {
        return _storyCounterService.Get();
    }
}