using EnglishTutorAI.Application.Models.Common;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.RenewAccess;

public class RenewAccessTokenCommand : IRequest<Result<string>>;