using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetDocument;

public class GetDocumentQuery : IRequest<DocumentResponse>
{
    public GetDocumentQuery(int index)
    {
        Index = index;
    }

    public int Index { get; }
}