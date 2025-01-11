namespace EnglishTutorAI.Application.Interfaces;

public interface ITranslateStartAgainService
{
    Task StartAgain(Guid userTranslateId);
}