using EnglishTutorAI.Application.Interfaces;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetDocumentCount;

public class GetDocumentCountQueryHandler : IRequestHandler<GetDocumentCountQuery, int>
{
    private readonly IDocumentCounterService _documentCounterService;

    public GetDocumentCountQueryHandler(IDocumentCounterService documentCounterService)
    {
        _documentCounterService = documentCounterService;
    }

    public Task<int> Handle(GetDocumentCountQuery request, CancellationToken cancellationToken)
    {
        return _documentCounterService.Get();
    }
}