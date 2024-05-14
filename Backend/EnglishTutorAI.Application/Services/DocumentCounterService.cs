using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

public class DocumentCounterService : IDocumentCounterService
{
    private readonly IRepository<Document> _documentRepository;

    public DocumentCounterService(IRepository<Document> documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public Task<int> Get()
    {
        return _documentRepository.Count();
    }
}