using EnglishTutorAI.Api.Constants;
using EnglishTutorAI.Api.Controllers.Attributes;
using EnglishTutorAI.Application.Handlers.GetUserAchievements;
using EnglishTutorAI.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishTutorAI.Api.Controllers;

[ApiRoute]
[Authorize]
[ApiController]
public class AchievementsController : Controller
{
    private readonly IMediator _mediator;

    public AchievementsController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet(Routes.Achievements.GetAchievements)]
    public Task<IEnumerable<UserAchievementResponse>> GetUserAchievements(Guid userId)
    {
        return _mediator.Send(new GetUserAchievementsQuery(userId));
    }
}