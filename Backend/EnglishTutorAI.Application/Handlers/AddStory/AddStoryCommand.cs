using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.AddStory;

public class AddStoryCommand : IRequest
{
    public StoryCreationRequest CreationRequest { get; }

    public AddStoryCommand(StoryCreationRequest creationRequest)
    {
        CreationRequest = creationRequest;
    }
}