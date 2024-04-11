using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Interfaces;

public interface IStoryRetrieverService
{
    Task<Story> GetStoryByIndex(int index);
}