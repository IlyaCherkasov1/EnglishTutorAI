using EnglishTutorAI.Application.Models.Documents;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetDocumentMistakeHistory;

public class GetDocumentMistakeHistoryQuery : IRequest<IEnumerable<DocumentMistakeHistoryItems>>
{
    public GetDocumentMistakeHistoryQuery(Guid sessionId)
    {
        SessionId = sessionId;
    }

    public Guid SessionId { get; set; }
}