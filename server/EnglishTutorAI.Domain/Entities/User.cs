using System.ComponentModel.DataAnnotations;
using EnglishTutorAI.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EnglishTutorAI.Domain.Entities;

public class User : IdentityUser<Guid>, IUserWithFirstName
{
    [MaxLength(50)]
    public required string FirstName { get; init; }
}