using MediatR;

namespace EnglishTutorAI.Application.Handlers.DeleteTranslate;

public class DeleteTranslateCommand : IRequest
{
    public DeleteTranslateCommand(Guid translateId)
    {
        TranslateId = translateId;
    }

    public Guid TranslateId { get; }
}