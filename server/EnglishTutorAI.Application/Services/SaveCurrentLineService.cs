using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

public class SaveCurrentLineService : ISaveCurrentLineService
{
    private readonly IRepository<Document> _documentRepository;

    public SaveCurrentLineService(IRepository<Document> documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task SaveCurrentLine(SaveCurrentLineRequest request)
    {
        var document = await _documentRepository.GetById(request.DocumentId);
        document.CurrentLine = request.CurrentLine;
    }
}