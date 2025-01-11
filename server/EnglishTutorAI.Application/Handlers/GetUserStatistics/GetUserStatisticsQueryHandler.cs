using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetUserStatistics;

public class GetUserStatisticsQueryHandler : IRequestHandler<GetUserStatisticsQuery, UserStatisticsResponse>
{
    private readonly IUserStatisticRetrievalService _userStatisticRetrievalService;

    public GetUserStatisticsQueryHandler(IUserStatisticRetrievalService userStatisticRetrievalService)
    {
        _userStatisticRetrievalService = userStatisticRetrievalService;
    }

    public Task<UserStatisticsResponse> Handle(GetUserStatisticsQuery request, CancellationToken cancellationToken)
    {
        return _userStatisticRetrievalService.Retrieve();
    }
}