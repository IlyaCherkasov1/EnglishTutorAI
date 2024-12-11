using AutoMapper;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetTranslateDetails;

public class GetTranslateDetailsQueryHandler : IRequestHandler<GetTranslateDetailsQuery, TranslateDetailsModel>
{
    private readonly ITranslateRetrievalService _translateRetrievalService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTranslateDetailsQueryHandler(
        ITranslateRetrievalService translateRetrievalService,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _translateRetrievalService = translateRetrievalService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TranslateDetailsModel> Handle(GetTranslateDetailsQuery request, CancellationToken cancellationToken)
    {
        var result = await _translateRetrievalService.GetTranslateDetailsById(request.TranslateId);

        await _unitOfWork.Commit();

        return _mapper.Map<TranslateDetailsModel>(result);
    }
}