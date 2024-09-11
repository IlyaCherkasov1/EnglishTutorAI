using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Models.Requests;
using EnglishTutorAI.Application.Models.Responses;
using EnglishTutorAI.Application.Utils;
using EnglishTutorAI.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EnglishTutorAI.Application.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<User> _userManager;
    private readonly JwtSettings _jwtSettings;
    private readonly IEmailService _emailService;

    public IdentityService(
        UserManager<User> userManager,
        IOptions<JwtSettings> jwtSettings,
        IEmailService emailService)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
        _emailService = emailService;
    }

    public async Task<Result> RegisterUser(UserRegisterRequest model)
    {
        var user = new User
        {
            FirstName = model.FirstName,
            Email = model.Email,
            UserName = model.Email,
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            return ResultBuilder.BuildFailed(result.Errors.Select(e => e.Description));
        }

        return ResultBuilder.BuildSucceeded();
    }

    public async Task<Result<LoginResponse>> LoginUser(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            return ResultBuilder.BuildFailed<LoginResponse>("There is no user with that Email address");
        }

        var result = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!result)
        {
            return ResultBuilder.BuildFailed<LoginResponse>("Invalid password");
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, request.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        var tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

        return ResultBuilder.BuildSucceeded(new LoginResponse
        {
            AccessToken = tokenAsString,
        });
    }
}