using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Models.Requests;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.Register;

public record RegisterCommand(UserRegisterRequest Request) : IRequest<Result>;