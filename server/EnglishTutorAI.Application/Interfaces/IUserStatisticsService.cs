namespace EnglishTutorAI.Application.Interfaces;

public interface IUserStatisticsService
{
    Task UpdateStatisticsAndSaveMessage(string translatedText, string correctedText);
}