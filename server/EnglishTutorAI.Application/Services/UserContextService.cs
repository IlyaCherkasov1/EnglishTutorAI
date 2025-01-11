using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class UserContextService : IUserContextService
{
    private readonly IAuthenticatedUserContext _authenticatedUserContext;

    public UserContextService(IAuthenticatedUserContext authenticatedUserContext)
    {
        _authenticatedUserContext = authenticatedUserContext;
    }

    public Guid UserId => _authenticatedUserContext.UserId ??
                          throw new UnauthorizedAccessException("User is not authenticated.");
}