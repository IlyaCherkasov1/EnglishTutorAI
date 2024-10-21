using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class DeleteDocumentService : IDeleteDocumentService
{
    private readonly IRepository<Document> _documentRepository;
    private readonly IRepository<DialogMessage> _chatMessageRepository;

    public DeleteDocumentService(IRepository<Document> documentRepository, IRepository<DialogMessage> chatRepository)
    {
        _documentRepository = documentRepository;
        _chatMessageRepository = chatRepository;
    }

    public async Task Delete(Guid documentId)
    {
        var document = await _documentRepository.GetById(documentId);
        _documentRepository.Delete(document);

        var chatList = await _chatMessageRepository.List(new DialogMessageByThreadIdSpecification(document.ThreadId));
        _chatMessageRepository.DeleteRange(chatList);
    }
}