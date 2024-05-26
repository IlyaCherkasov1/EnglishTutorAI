using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.CreateAssistance;

public class CreateAssistanceCommandHandler : IRequestHandler<CreateAssistanceCommand, CreateAssistantResponse>
{
    private readonly IAssistanceCreationService _assistanceCreationService;

    public CreateAssistanceCommandHandler(IAssistanceCreationService assistanceCreationService)
    {
        _assistanceCreationService = assistanceCreationService;
    }

    public Task<CreateAssistantResponse> Handle(CreateAssistanceCommand request, CancellationToken cancellationToken)
    {
        return _assistanceCreationService.Create();
    }
}