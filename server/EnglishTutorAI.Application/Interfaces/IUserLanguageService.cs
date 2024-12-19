using EnglishTutorAI.Domain.Enums;

namespace EnglishTutorAI.Application.Interfaces;

public interface IUserLanguageService
{
    Task ChangeLanguage(Language language);
}