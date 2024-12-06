using MediatR;

namespace EnglishTutorAI.Application.Handlers.HandleDocumentStart;

public record HandleDocumentStartCommand(Guid UserDocumentId) : IRequest;