using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class DeleteDocumentService : IDeleteDocumentService
{
    private readonly IRepository<Document> _documentRepository;

    public DeleteDocumentService(IRepository<Document> documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task Delete(Guid documentId)
    {
        var document = await _documentRepository.GetById(documentId);
        _documentRepository.Delete(document);
    }
}