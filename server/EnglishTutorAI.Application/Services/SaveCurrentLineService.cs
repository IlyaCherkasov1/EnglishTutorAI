using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class SaveCurrentLineService : ISaveCurrentLineService
{
    private readonly IRepository<UserDocument> _userDocumentRepository;

    public SaveCurrentLineService(IRepository<UserDocument> userDocumentRepository)
    {
        _userDocumentRepository = userDocumentRepository;
    }

    public async Task SaveCurrentLine(SaveCurrentLineRequest request)
    {
        var userDocument = await _userDocumentRepository.GetById(request.UserDocumentId);
        userDocument.CurrentLine = request.CurrentLine;
    }
}