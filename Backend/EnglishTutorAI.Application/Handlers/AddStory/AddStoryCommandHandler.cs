using EnglishTutorAI.Application.Interfaces;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.AddStory;

public class AddStoryCommandHandler : IRequestHandler<AddStoryCommand>
{
    private readonly IStoryService _storyService;
    private readonly IUnitOfWork _unitOfWork;

    public AddStoryCommandHandler(IStoryService storyService, IUnitOfWork unitOfWork)
    {
        _storyService = storyService;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AddStoryCommand request, CancellationToken cancellationToken)
    {
        await _storyService.AddStory(request.CreationRequest);
        await _unitOfWork.Commit();
    }
}