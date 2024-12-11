using MediatR;

namespace EnglishTutorAI.Application.Handlers.HandleTranslateCompletion;

public record HandleTranslateCompletionCommand(Guid UserTranslateId) : IRequest;