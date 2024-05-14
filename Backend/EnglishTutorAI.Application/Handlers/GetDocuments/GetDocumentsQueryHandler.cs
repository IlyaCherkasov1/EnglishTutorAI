﻿using AutoMapper;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetDocuments;

public class GetDocumentsQueryHandler : IRequestHandler<GetDocumentsQuery, IReadOnlyList<DocumentListItem>>
{
    private readonly IDocumentRetrievalService _documentRetrievalService;
    private readonly IMapper _mapper;

    public GetDocumentsQueryHandler(IDocumentRetrievalService documentRetrievalService, IMapper mapper)
    {
        _documentRetrievalService = documentRetrievalService;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<DocumentListItem>> Handle(GetDocumentsQuery request, CancellationToken cancellationToken)
    {
        var result = await _documentRetrievalService.GetAllDocuments();

        return _mapper.Map<IReadOnlyList<DocumentListItem>>(result);
    }
}