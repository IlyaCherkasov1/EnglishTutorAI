using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetCompletedDocuments;

public class GetCompletedDocumentsQuery : IRequest<SearchResult<CompletedDocumentListItem>>
{
    public GetCompletedDocumentsQuery(PaginationSearchModel paginationSearchModel)
    {
        PaginationSearchModel = paginationSearchModel;
    }

    public PaginationSearchModel PaginationSearchModel { get; set; }
}