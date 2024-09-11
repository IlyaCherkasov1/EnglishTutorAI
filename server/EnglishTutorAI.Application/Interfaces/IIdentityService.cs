using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Models.Requests;
using EnglishTutorAI.Application.Models.Responses;

namespace EnglishTutorAI.Application.Interfaces;

public interface IIdentityService
{
    Task<Result> RegisterUser(UserRegisterRequest registerRequest);

    Task<Result<LoginResponse>> LoginUser(LoginRequest request);
}