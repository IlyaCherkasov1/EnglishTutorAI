using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Interfaces;

public interface IStoryRetrievalService
{
    Task<Story> GetStoryByIndex(int index);
    Task<IReadOnlyList<StoryListItem>> GetAllStories();
}