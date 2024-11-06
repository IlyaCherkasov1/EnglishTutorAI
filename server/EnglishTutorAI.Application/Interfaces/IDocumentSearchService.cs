using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Models.Documents;

namespace EnglishTutorAI.Application.Interfaces;

public interface IDocumentSearchService
{
    Task<IEnumerable<DocumentListItem>> Search(DocumentsSearchModel model);
}