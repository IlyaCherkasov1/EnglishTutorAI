using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;

namespace EnglishTutorAI.Application.Interfaces;

public interface ITranslateCompletionService
{
    Task Save(Guid userTranslateId);

    Task<SearchResult<CompletedTranslateListItem>> GetCompletedTranslates(PaginationSearchModel model);
}