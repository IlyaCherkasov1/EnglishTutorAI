﻿using EnglishTutorAI.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace EnglishTutorAI.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public required string FirstName { get; init; }

    public UserStatistics UserStatistics { get; init; } = null!;

    public ICollection<UserTranslate> UserTranslates { get; init; } = null!;

    public Language PreferredLanguage { get; set; } = Language.Russian;
}