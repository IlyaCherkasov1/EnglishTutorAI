using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.AddTranslate;

public class AddTranslateCommand : IRequest
{
    public TranslateCreationRequest CreationRequest { get; }

    public AddTranslateCommand(TranslateCreationRequest creationRequest)
    {
        CreationRequest = creationRequest;
    }
}