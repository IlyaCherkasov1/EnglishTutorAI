using EnglishTutorAI.Application.Models.Translates;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetTranslateMistakeHistory;

public class GetTranslateMistakeHistoryQuery : IRequest<IEnumerable<TranslateMistakeHistoryItems>>
{
    public GetTranslateMistakeHistoryQuery(Guid userTranslateId)
    {
        UserTranslateId = userTranslateId;
    }

    public Guid UserTranslateId { get; set; }
}