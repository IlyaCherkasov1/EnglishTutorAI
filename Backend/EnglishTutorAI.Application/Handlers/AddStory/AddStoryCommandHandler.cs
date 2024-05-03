using EnglishTutorAI.Application.Interfaces;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.AddStory;

public class AddStoryCommandHandler : IRequestHandler<AddStoryCommand>
{
    private readonly IStoryCreationService _storyCreationService;
    private readonly IUnitOfWork _unitOfWork;

    public AddStoryCommandHandler(IStoryCreationService storyCreationService, IUnitOfWork unitOfWork)
    {
        _storyCreationService = storyCreationService;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AddStoryCommand request, CancellationToken cancellationToken)
    {
        await _storyCreationService.AddStory(request.CreationRequest);
        await _unitOfWork.Commit();
    }
}