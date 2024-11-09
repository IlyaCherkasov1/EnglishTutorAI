using AutoMapper;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetDocumentDetails;

public class GetDocumentDetailsQueryHandler : IRequestHandler<GetDocumentDetailsQuery, DocumentResponse>
{
    private readonly IDocumentRetrievalService _documentRetrievalService;

    public GetDocumentDetailsQueryHandler(IDocumentRetrievalService documentRetrievalService)
    {
        _documentRetrievalService = documentRetrievalService;
    }

    public async Task<DocumentResponse> Handle(GetDocumentDetailsQuery request, CancellationToken cancellationToken)
    {
        return await _documentRetrievalService.GetDocumentById(request.Id);
    }
}