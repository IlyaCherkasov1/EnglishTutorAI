using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Interfaces;

public interface IUserAchievementService
{
    Task<IEnumerable<UserAchievementResponse>> GetUserAchievements();
}