using System.ComponentModel.DataAnnotations;
using EnglishTutorAI.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EnglishTutorAI.Domain.Entities;

public class User : IdentityUser, IUserWithFirstName
{
    [MaxLength(50)]
    public string FirstName { get; init; }
}