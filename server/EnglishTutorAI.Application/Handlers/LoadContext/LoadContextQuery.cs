using EnglishTutorAI.Application.Models.Responses;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.LoadContext;

public class LoadContextQuery : IRequest<ContextResponse>;