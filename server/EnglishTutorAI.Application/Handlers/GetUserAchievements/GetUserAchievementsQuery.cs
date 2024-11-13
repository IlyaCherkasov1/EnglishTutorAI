using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetUserAchievements;

public record GetUserAchievementsQuery : IRequest<IEnumerable<UserAchievementResponse>>;