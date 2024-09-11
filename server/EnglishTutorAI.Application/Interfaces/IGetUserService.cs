using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Interfaces;

public interface IGetUserService
{
    Task<User?> GetUser();
}