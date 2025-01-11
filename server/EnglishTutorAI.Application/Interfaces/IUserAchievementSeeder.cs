using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Interfaces;

public interface IUserAchievementSeeder
{
    Task Seed(User user);
}