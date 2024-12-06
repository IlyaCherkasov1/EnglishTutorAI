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
    private readonly IUserContextService _userContextService;

    public DocumentSearchService(
        IRepository<Document> documentRepository,
        IUserContextService userContextService)
    {
        _documentRepository = documentRepository;
        _userContextService = userContextService;
    }

    public async Task<SearchResult<DocumentListItem>> Search(DocumentsSearchModel model)
    {
        return await _documentRepository.Search(
            new DocumentListSearchSpecification(model, _userContextService.UserId));
    }

    public async Task<DocumentListItem?> GetNextDocument(NextDocumentSearchModel model)
    {
        return await _documentRepository.GetFirstOrDefault(
            new NextDocumentSearchSpecification(model, _userContextService.UserId));
    }
}