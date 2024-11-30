using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Models.Documents;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetDocuments;

public class GetDocumentsQuery : IRequest<SearchResult<DocumentListItem>>
{
    public GetDocumentsQuery(DocumentsSearchModel model)
    {
        Model = model;
    }

    public DocumentsSearchModel Model { get; }
}