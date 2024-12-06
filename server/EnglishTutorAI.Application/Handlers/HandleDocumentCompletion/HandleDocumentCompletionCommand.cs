using MediatR;

namespace EnglishTutorAI.Application.Handlers.HandleDocumentCompletion;

public record HandleDocumentCompletionCommand(Guid UserDocumentId) : IRequest;