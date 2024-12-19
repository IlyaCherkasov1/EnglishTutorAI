using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Domain.Entities;
using EnglishTutorAI.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class UserLanguageService : IUserLanguageService
{
    private readonly UserManager<User> _userManager;
    private readonly IUserContextService _userContextService;

    public UserLanguageService(UserManager<User> userManager, IUserContextService userContextService)
    {
        _userManager = userManager;
        _userContextService = userContextService;
    }

    public async Task ChangeLanguage(Language language)
    {
        var user = await _userManager.FindByIdAsync(_userContextService.UserId.ToString());

        user!.PreferredLanguage = language;
        await _userManager.UpdateAsync(user);
    }
}