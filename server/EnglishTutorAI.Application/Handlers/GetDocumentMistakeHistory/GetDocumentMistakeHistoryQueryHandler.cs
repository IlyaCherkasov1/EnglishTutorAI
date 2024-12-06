using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models.Documents;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetDocumentMistakeHistory;

public class GetDocumentMistakeHistoryQueryHandler :
    IRequestHandler<GetDocumentMistakeHistoryQuery, IEnumerable<DocumentMistakeHistoryItems>>
{
    private readonly IDocumentMistakeHistoryService _documentMistakeHistoryService;

    public GetDocumentMistakeHistoryQueryHandler(IDocumentMistakeHistoryService documentMistakeHistoryService)
    {
        _documentMistakeHistoryService = documentMistakeHistoryService;
    }

    public Task<IEnumerable<DocumentMistakeHistoryItems>> Handle(
        GetDocumentMistakeHistoryQuery request, CancellationToken cancellationToken)
    {
        return _documentMistakeHistoryService.Get(request.UserDocumentId);
    }
}