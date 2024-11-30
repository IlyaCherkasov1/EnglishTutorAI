using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Models.Documents;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class DocumentSearchService : IDocumentSearchService
{
    private readonly IRepository<Document> _documentRepository;

    public DocumentSearchService(IRepository<Document> documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task<SearchResult<DocumentListItem>> Search(DocumentsSearchModel model)
    {
        return await _documentRepository.Search(new DocumentListSearchSpecification(model));
    }
}