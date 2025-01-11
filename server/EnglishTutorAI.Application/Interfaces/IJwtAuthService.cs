using System.Security.Claims;
using EnglishTutorAI.Application.Models.Common;

namespace EnglishTutorAI.Application.Interfaces;

public interface IJwtAuthService
{
    string GenerateAccessToken(List<Claim> claims);

    Task<Result<string>> RenewAccessToken();
}