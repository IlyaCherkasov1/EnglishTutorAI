using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Models.Translates;

namespace EnglishTutorAI.Application.Interfaces;

public interface ITranslateSearchService
{
    Task<SearchResult<TranslateListItem>> Search(TranslateSearchModel model);

    Task<TranslateListItem?> GetNextTranslate(NextTranslateSearchModel model);
}