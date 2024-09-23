using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Models.Requests;

namespace EnglishTutorAI.Application.Interfaces;

public interface IIdentityService
{
    Task<Result> RegisterUser(UserRegisterRequest registerRequest);

    Task<Result<string>> LoginUser(LoginRequest request);

    Task Logout();
}