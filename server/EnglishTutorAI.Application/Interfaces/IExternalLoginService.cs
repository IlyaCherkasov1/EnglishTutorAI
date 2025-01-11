using Microsoft.AspNetCore.Authentication;

namespace EnglishTutorAI.Application.Interfaces;

public interface IExternalLoginService
{
    Task<AuthenticationProperties> Configure(string provider, string redirectUrl);
}