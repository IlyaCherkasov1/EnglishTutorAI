using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetStories;

public class GetStoriesQuery : IRequest<IReadOnlyList<StoryListItem>>
{
}