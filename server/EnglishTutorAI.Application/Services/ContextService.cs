using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models.Responses;
using EnglishTutorAI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using EnglishTutorAI.Application.Attributes;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class ContextService : IContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<User> _userManager;

    public ContextService(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    public async Task<ContextResponse> Load()
    {
        var userPrincipal = _httpContextAccessor.HttpContext!.User;
        var user = (await _userManager.GetUserAsync(userPrincipal));

        if (user == null)
        {
            return new ContextResponse();
        }

        var roles = await _userManager.GetRolesAsync(user);

        return new ContextResponse
        {
            FirstName = user.FirstName,
            IsAuthenticated = userPrincipal.Identity!.IsAuthenticated,
            RoleName = roles,
            Email = user.Email,
        };
    }
}