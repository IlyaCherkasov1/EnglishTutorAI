using EnglishTutorAI.Api.Constants;
using EnglishTutorAI.Api.Controllers.Attributes;
using EnglishTutorAI.Application.Constants;
using EnglishTutorAI.Application.Handlers.AddDocument;
using EnglishTutorAI.Application.Handlers.DeleteDocument;
using EnglishTutorAI.Application.Handlers.GetConversationThread;
using EnglishTutorAI.Application.Handlers.GetDocumentDetails;
using EnglishTutorAI.Application.Handlers.GetDocuments;
using EnglishTutorAI.Application.Handlers.HandleDocumentCompletion;
using EnglishTutorAI.Application.Handlers.SaveProgress;
using EnglishTutorAI.Application.Models;
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
    [Authorize(Roles = UserRoles.Admin)]
    public Task AddDocument(DocumentCreationRequest request)
    {
        return _mediator.Send(new AddDocumentCommand(request));
    }

    [HttpGet(Routes.Document.GetDocument)]
    public Task<IEnumerable<DocumentListItem>> Get([FromQuery]DocumentsSearchModel model)
    {
        return _mediator.Send(new GetDocumentsQuery(model));
    }

    [HttpGet(Routes.Document.GetDocumentDetails)]
    public async Task<IActionResult> GetDocumentDetails(string id)
    {
        if (!Guid.TryParse(id, out var documentId))
        {
            return NotFound("Document not found.");
        }

        var documentResponse = await _mediator.Send(new GetDocumentDetailsQuery(documentId));

        return Ok(documentResponse);
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
    [Authorize(Roles = UserRoles.Admin)]
    public Task DeleteDocument(Guid documentId)
    {
        return _mediator.Send(new DeleteDocumentCommand(documentId));
    }

    [HttpPost(Routes.Document.HandleDocumentCompletion)]
    public Task HandleDocumentCompletion(Guid documentId)
    {
        return _mediator.Send(new HandleDocumentCompletionCommand(documentId));
    }
}