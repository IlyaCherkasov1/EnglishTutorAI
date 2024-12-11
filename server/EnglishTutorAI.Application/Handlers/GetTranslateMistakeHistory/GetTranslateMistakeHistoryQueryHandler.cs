using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models.Translates;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetTranslateMistakeHistory;

public class GetTranslateMistakeHistoryQueryHandler :
    IRequestHandler<GetTranslateMistakeHistoryQuery, IEnumerable<TranslateMistakeHistoryItems>>
{
    private readonly ITranslateMistakeHistoryService _translateMistakeHistoryService;

    public GetTranslateMistakeHistoryQueryHandler(ITranslateMistakeHistoryService translateMistakeHistoryService)
    {
        _translateMistakeHistoryService = translateMistakeHistoryService;
    }

    public Task<IEnumerable<TranslateMistakeHistoryItems>> Handle(
        GetTranslateMistakeHistoryQuery request, CancellationToken cancellationToken)
    {
        return _translateMistakeHistoryService.Get(request.UserTranslateId);
    }
}