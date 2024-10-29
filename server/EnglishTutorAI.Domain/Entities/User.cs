using System.ComponentModel.DataAnnotations;
using EnglishTutorAI.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EnglishTutorAI.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public required string FirstName { get; init; }
}