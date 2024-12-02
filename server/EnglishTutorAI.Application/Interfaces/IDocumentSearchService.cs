using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Models.Documents;

namespace EnglishTutorAI.Application.Interfaces;

public interface IDocumentSearchService
{
    Task<SearchResult<DocumentListItem>> Search(DocumentsSearchModel model);

    Task<DocumentListItem?> GetNextDocument(NextDocumentSearchModel model);
}