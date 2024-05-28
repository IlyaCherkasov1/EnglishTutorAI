using System.Text.RegularExpressions;
using EnglishTutorAI.Api.Constants;
using EnglishTutorAI.Api.Controllers.Attributes;
using EnglishTutorAI.Application.Handlers.AddDocument;
using EnglishTutorAI.Application.Handlers.GetDocument;
using EnglishTutorAI.Application.Handlers.GetDocumentCount;
using EnglishTutorAI.Application.Handlers.GetDocumentDetails;
using EnglishTutorAI.Application.Handlers.GetDocuments;
using EnglishTutorAI.Application.Handlers.SaveProgress;
using EnglishTutorAI.Application.Handlers.SplitSentences;
using EnglishTutorAI.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnglishTutorAI.Api.Controllers;

[ApiController]
[ApiRoute]
public class DocumentController : ControllerBase
{
    private readonly IMediator _mediator;

    public DocumentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Routes.Document.GetDocumentByIndex)]
    public Task<DocumentResponse> GetDocumentByIndex(int index)
    {
        return _mediator.Send(new GetDocumentQuery(index));
    }

    [HttpGet(Routes.Document.Count)]
    public Task<int> GetDocumentCount()
    {
        return _mediator.Send(new GetDocumentCountQuery());
    }

    [HttpPost(Routes.Document.AddDocument)]
    public Task AddDocument(DocumentCreationRequest request)
    {
        return _mediator.Send(new AddDocumentCommand(request));
    }

    [HttpGet(Routes.Document.GetDocument)]
    public Task<IReadOnlyList<DocumentListItem>> Get()
    {
        return _mediator.Send(new GetDocumentsQuery());
    }

    [HttpGet(Routes.Document.GetDocumentDetails)]
    public Task<DocumentResponse> GetDocumentDetails(Guid id)
    {
        return _mediator.Send(new GetDocumentDetailsQuery(id));
    }

    [HttpPost(Routes.Document.SplitDocumentContent)]
    public Task<List<string>> SplitDocumentContent(SplitDocumentContentRequest request)
    {
        return _mediator.Send(new SplitSentencesQuery(request.Text!));
    }

    [HttpPost(Routes.Document.SaveCurrentLine)]
    public Task SaveCurrentLine(SaveCurrentLineRequest request)
    {
        return _mediator.Send(new SaveCurrentLineCommand(request));
    }
}