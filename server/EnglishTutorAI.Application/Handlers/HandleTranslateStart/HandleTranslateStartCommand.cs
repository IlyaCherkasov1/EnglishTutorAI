using MediatR;

namespace EnglishTutorAI.Application.Handlers.HandleTranslateStart;

public record HandleTranslateStartCommand(Guid UserTranslateId) : IRequest;