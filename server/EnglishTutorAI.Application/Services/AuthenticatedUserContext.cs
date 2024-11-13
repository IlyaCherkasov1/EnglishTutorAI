using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Extensions;
using EnglishTutorAI.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class AuthenticatedUserContext : IAuthenticatedUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticatedUserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserId => _httpContextAccessor.HttpContext?.GetUserId();
}