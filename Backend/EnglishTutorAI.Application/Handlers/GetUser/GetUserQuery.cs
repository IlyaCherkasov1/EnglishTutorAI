using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetUser;

public class GetUserQuery : IRequest<IdentityUserResponse?>
{
}