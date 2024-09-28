using MediatR;
using Microsoft.AspNetCore.Authentication;

namespace EnglishTutorAI.Application.Handlers.ExternalLogin;

public record ExternalLoginCommand(string Provider, string RedirectUrl) : IRequest<AuthenticationProperties>;