using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetUserAchievements;

public record GetUserAchievementsQuery(Guid UserId) : IRequest<IEnumerable<UserAchievementResponse>>;