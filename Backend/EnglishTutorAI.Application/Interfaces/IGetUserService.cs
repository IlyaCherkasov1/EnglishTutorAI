using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EnglishTutorAI.Application.Interfaces;

public interface IGetUserService
{
    Task<User?> GetUser();
}