using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Models.Requests;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.Login;

public record LoginCommand(LoginRequest Request) : IRequest<Result<string>>;