using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models.Responses;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.LoadContext;

public class LoadContextQueryHandler : IRequestHandler<LoadContextQuery, ContextResponse>
{
    private readonly IContextService _contextService;

    public LoadContextQueryHandler(IContextService contextService)
    {
        _contextService = contextService;
    }

    public async Task<ContextResponse> Handle(LoadContextQuery request, CancellationToken cancellationToken)
    {
        return await _contextService.Load();
    }
}