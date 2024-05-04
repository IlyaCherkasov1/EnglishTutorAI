using AutoMapper;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetDocumentDetails;

public class GetDocumentDetailsQueryHandler : IRequestHandler<GetDocumentDetailsQuery, DocumentResponse>
{
    private readonly IDocumentRetrievalService _documentRetrievalService;
    private readonly IMapper _mapper;

    public GetDocumentDetailsQueryHandler(IMapper mapper, IDocumentRetrievalService documentRetrievalService)
    {
        _mapper = mapper;
        _documentRetrievalService = documentRetrievalService;
    }

    public async Task<DocumentResponse> Handle(GetDocumentDetailsQuery request, CancellationToken cancellationToken)
    {
        var result = await _documentRetrievalService.GetDocumentById(request.Id);

        return _mapper.Map<DocumentResponse>(result);
    }
}