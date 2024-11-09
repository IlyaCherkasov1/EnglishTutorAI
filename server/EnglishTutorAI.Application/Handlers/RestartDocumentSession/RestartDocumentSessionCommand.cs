using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.RestartDocumentSession;

public record RestartDocumentSessionCommand(RestartDocumentSessionRequest Request) : IRequest<Guid>;