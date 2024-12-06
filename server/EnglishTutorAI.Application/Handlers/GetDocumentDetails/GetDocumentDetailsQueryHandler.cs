using AutoMapper;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetDocumentDetails;

public class GetDocumentDetailsQueryHandler : IRequestHandler<GetDocumentDetailsQuery, DocumentDetailsModel>
{
    private readonly IDocumentRetrievalService _documentRetrievalService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDocumentDetailsQueryHandler(
        IDocumentRetrievalService documentRetrievalService,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _documentRetrievalService = documentRetrievalService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DocumentDetailsModel> Handle(GetDocumentDetailsQuery request, CancellationToken cancellationToken)
    {
        var result = await _documentRetrievalService.GetDocumentDetailsById(request.DocumentId);

        await _unitOfWork.Commit();

        return _mapper.Map<DocumentDetailsModel>(result);
    }
}