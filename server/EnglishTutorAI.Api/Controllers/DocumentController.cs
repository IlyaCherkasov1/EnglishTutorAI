using EnglishTutorAI.Api.Constants;
using EnglishTutorAI.Api.Controllers.Attributes;
using EnglishTutorAI.Application.Handlers.AddDocument;
using EnglishTutorAI.Application.Handlers.DeleteDocument;
using EnglishTutorAI.Application.Handlers.GetConversationThread;
using EnglishTutorAI.Application.Handlers.GetDocumentDetails;
using EnglishTutorAI.Application.Handlers.GetDocuments;
using EnglishTutorAI.Application.Handlers.SaveProgress;
using EnglishTutorAI.Application.Handlers.SplitSentences;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Models.Documents;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishTutorAI.Api.Controllers;

[ApiRoute]
[Authorize]
[ApiController]
public class DocumentController : ControllerBase
{
    private readonly IMediator _mediator;

    public DocumentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(Routes.Document.AddDocument)]
    public Task AddDocument(DocumentCreationRequest request)
    {
        return _mediator.Send(new AddDocumentCommand(request));
    }

    [HttpGet(Routes.Document.GetDocument)]
    public Task<SearchResult<DocumentListItem>> Get([FromQuery]DocumentsSearchModel model)
    {
        return _mediator.Send(new GetDocumentsQuery(model));
    }

    [HttpGet(Routes.Document.GetDocumentDetails)]
    public Task<DocumentResponse> GetDocumentDetails(Guid id)
    {
        return _mediator.Send(new GetDocumentDetailsQuery(id));
    }

    [HttpPost(Routes.Document.SplitDocumentContent)]
    public Task<IEnumerable<string>> SplitDocumentContent(SplitDocumentContentRequest request)
    {
        return _mediator.Send(new SplitSentencesQuery(request.Text!));
    }

    [HttpPost(Routes.Document.SaveCurrentLine)]
    public Task SaveCurrentLine(SaveCurrentLineRequest request)
    {
        return _mediator.Send(new SaveCurrentLineCommand(request));
    }

    [HttpGet(Routes.Document.GetConversationThread)]
    public Task<IEnumerable<ChatMessageResponse>> GetConversationThread(string threadId)
    {
        return _mediator.Send(new GetConversationThreadCommand(threadId));
    }

    [HttpPost(Routes.Document.DeleteDocument)]
    public Task DeleteDocument(Guid documentId)
    {
        return _mediator.Send(new DeleteDocumentCommand(documentId));
    }
}