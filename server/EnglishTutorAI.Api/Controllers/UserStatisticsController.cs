using EnglishTutorAI.Api.Constants;
using EnglishTutorAI.Api.Controllers.Attributes;
using EnglishTutorAI.Application.Handlers.GetUserStatistics;
using EnglishTutorAI.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishTutorAI.Api.Controllers;

[ApiRoute]
[Authorize]
[ApiController]
public class UserStatisticsController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserStatisticsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Routes.UserStatistics.GetStatistics)]
    public Task<UserStatisticsResponse> GetUserStatistics()
    {
        return _mediator.Send(new GetUserStatisticsQuery());
    }
}