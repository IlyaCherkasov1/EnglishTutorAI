using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Models.Translates;

namespace EnglishTutorAI.Application.Interfaces;

public interface IMistakeHistorySearchService
{
    Task<SearchResult<MistakeHistoryItems>> Search(PaginationSearchModel model);
}