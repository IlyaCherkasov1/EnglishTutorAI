using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetUserStatistics;

public record GetUserStatisticsQuery : IRequest<UserStatisticsResponse>;