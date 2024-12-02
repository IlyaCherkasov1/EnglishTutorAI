using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Documents;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetNextDocument;

public class GetNextDocumentQuery : IRequest<DocumentListItem?>
{
    public GetNextDocumentQuery(NextDocumentSearchModel model)
    {
        Model = model;
    }

    public NextDocumentSearchModel Model { get; set; }
}