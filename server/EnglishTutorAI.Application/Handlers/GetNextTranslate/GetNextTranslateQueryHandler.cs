using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetNextTranslate;

public class GetNextTranslateQueryHandler : IRequestHandler<GetNextTranslateQuery, TranslateListItem?>
{
    private readonly ITranslateSearchService _translateSearchService;

    public GetNextTranslateQueryHandler(ITranslateSearchService translateSearchService)
    {
        _translateSearchService = translateSearchService;
    }

    public async Task<TranslateListItem?> Handle(GetNextTranslateQuery request, CancellationToken cancellationToken)
    {
        return await _translateSearchService.GetNextTranslate(request.Model);
    }
}