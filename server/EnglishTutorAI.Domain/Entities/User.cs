using System.ComponentModel.DataAnnotations;
using EnglishTutorAI.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EnglishTutorAI.Domain.Entities;

public class User : IdentityUser<Guid>, IUserWithFirstName
{
    [EmailAddress]
    public override required string Email { get; set; }
    public override required string UserName { get; set; }
    public required string FirstName { get; init; }
}