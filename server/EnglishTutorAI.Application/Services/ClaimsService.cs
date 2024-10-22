using System.Security.Claims;
using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class ClaimsService : IClaimsService
{
    public List<Claim> CreateUserClaims(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        return claims;
    }
}