using System.Security.Claims;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Interfaces;

public interface IClaimsService
{
    List<Claim> CreateUserClaims(User user);
}