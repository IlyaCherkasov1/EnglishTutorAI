using EnglishTutorAI.Application.Models;

namespace EnglishTutorAI.Application.Interfaces;

public interface IStatisticsService
{
    Task SaveStatisticsAndMessage(SaveStatisticsAndMessageModel model);
}