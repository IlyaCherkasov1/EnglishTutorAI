using EnglishTutorAI.Domain.Enums;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.ChangeLanguage;

public record ChangeLanguageCommand(Language Language) : IRequest;