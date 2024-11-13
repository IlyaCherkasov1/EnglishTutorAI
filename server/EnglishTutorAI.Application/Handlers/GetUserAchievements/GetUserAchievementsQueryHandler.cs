using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetUserAchievements;

public class GetUserAchievementsQueryHandler : IRequestHandler<GetUserAchievementsQuery, IEnumerable<UserAchievementResponse>>
{
    private readonly IUserAchievementService _userAchievementService;

    public GetUserAchievementsQueryHandler(IUserAchievementService userAchievementService)
    {
        _userAchievementService = userAchievementService;
    }

    public Task<IEnumerable<UserAchievementResponse>> Handle(GetUserAchievementsQuery request, CancellationToken cancellationToken)
    {
        return _userAchievementService.GetUserAchievements();
    }
}