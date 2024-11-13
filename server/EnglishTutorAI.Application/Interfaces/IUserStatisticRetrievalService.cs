using EnglishTutorAI.Application.Models;

namespace EnglishTutorAI.Application.Interfaces;

public interface IUserStatisticRetrievalService
{
    Task<UserStatisticsResponse> Retrieve();
}