using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;

namespace EnglishTutorAI.Application.Interfaces;

public interface IDocumentCompletionService
{
    Task Save(Guid userDocumentId);

    Task<SearchResult<CompletedDocumentListItem>> GetCompletedDocuments(PaginationSearchModel model);
}