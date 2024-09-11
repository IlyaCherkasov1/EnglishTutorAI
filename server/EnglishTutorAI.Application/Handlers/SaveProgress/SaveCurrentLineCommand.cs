using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.SaveProgress;

public class SaveCurrentLineCommand : IRequest
{
    public SaveCurrentLineCommand(SaveCurrentLineRequest saveCurrentLineRequest)
    {
        SaveCurrentLineRequest = saveCurrentLineRequest;
    }

    public SaveCurrentLineRequest SaveCurrentLineRequest { get; }
}