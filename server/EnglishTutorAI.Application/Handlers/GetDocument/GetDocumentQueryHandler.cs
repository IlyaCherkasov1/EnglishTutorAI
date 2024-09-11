using AutoMapper;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetDocument;

public class GetDocumentQueryHandler : IRequestHandler<GetDocumentQuery, DocumentResponse>
{
    private readonly IDocumentRetrievalService _documentRetrievalService;
    private readonly IMapper _mapper;

    public GetDocumentQueryHandler(IMapper mapper, IDocumentRetrievalService documentRetrievalService)
    {
        _mapper = mapper;
        _documentRetrievalService = documentRetrievalService;
    }

    public async Task<DocumentResponse> Handle(GetDocumentQuery request, CancellationToken cancellationToken)
    {
        var document = await _documentRetrievalService.GetDocumentByIndex(request.Index);

        return _mapper.Map<DocumentResponse>(document);
    }
}