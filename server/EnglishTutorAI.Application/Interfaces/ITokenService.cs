﻿using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(User user);

    string GenerateRefreshToken();

    Task<Result<string>> RenewAccessToken();
}