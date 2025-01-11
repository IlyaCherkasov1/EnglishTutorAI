namespace EnglishTutorAI.Application.Interfaces;

public interface IAuthenticatedUserContext
{
    Guid? UserId { get; }
}