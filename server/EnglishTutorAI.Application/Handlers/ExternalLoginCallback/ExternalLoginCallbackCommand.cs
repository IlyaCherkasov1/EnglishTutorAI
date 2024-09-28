using EnglishTutorAI.Application.Models.Common;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.ExternalLoginCallback;

public class ExternalLoginCallbackCommand : IRequest<Result>;