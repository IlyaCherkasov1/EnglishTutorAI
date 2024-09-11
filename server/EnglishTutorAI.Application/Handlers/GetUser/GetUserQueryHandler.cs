using AutoMapper;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, IdentityUserResponse?>
{
    private readonly IGetUserService _getUserService;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(IGetUserService getUserService, IMapper mapper)
    {
        _getUserService = getUserService;
        _mapper = mapper;
    }

    public async Task<IdentityUserResponse?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var result = await _getUserService.GetUser();

        return _mapper.Map<IdentityUserResponse>(result);
    }
}