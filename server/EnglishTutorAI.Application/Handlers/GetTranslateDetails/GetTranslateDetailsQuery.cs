using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetTranslateDetails;

public class GetTranslateDetailsQuery : IRequest<TranslateDetailsModel>
{
    public GetTranslateDetailsQuery(Guid translateId)
    {
        TranslateId = translateId;
    }

    public Guid TranslateId { get; }
}