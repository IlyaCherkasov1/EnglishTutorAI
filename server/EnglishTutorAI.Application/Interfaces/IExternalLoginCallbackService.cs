using EnglishTutorAI.Application.Models.Common;

namespace EnglishTutorAI.Application.Interfaces;

public interface IExternalLoginCallbackService
{
    Task<Result> Login();
}