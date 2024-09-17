using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

public class DeleteDocumentService : IDeleteDocumentService
{
    private readonly IRepository<Document> _documentRepository;
    private readonly IRepository<ChatMessage> _chatMessageRepository;

    public DeleteDocumentService(IRepository<Document> documentRepository, IRepository<ChatMessage> chatRepository)
    {
        _documentRepository = documentRepository;
        _chatMessageRepository = chatRepository;
    }

    public async Task Delete(Guid documentId)
    {
        var document = await _documentRepository.GetById(documentId);
        _documentRepository.Delete(document);

        var chatList = await _chatMessageRepository.List(new ChatMessageByThreadIdSpecification(document.ThreadId));
        _chatMessageRepository.DeleteRange(chatList);
    }
}