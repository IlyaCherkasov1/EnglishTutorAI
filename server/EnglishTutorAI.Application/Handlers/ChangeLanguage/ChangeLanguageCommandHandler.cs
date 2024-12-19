using EnglishTutorAI.Application.Interfaces;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.ChangeLanguage;

public class ChangeLanguageCommandHandler : IRequestHandler<ChangeLanguageCommand>
{
    private readonly IUserLanguageService _userLanguageService;

    public ChangeLanguageCommandHandler(IUserLanguageService userLanguageService)
    {
        _userLanguageService = userLanguageService;
    }

    public Task Handle(ChangeLanguageCommand request, CancellationToken cancellationToken)
    {
        return _userLanguageService.ChangeLanguage(request.Language);
    }
}