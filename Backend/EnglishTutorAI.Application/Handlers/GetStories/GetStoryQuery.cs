using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetStories;

public class GetStoryQuery : IRequest<StoryResponse>
{
    public GetStoryQuery(int index)
    {
        Index = index;
    }

    public int Index { get; }
}