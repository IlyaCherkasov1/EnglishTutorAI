using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Interfaces;

public interface IStoryService
{
    Task<Story> GetStoryByIndex(int index);

    Task AddStory(StoryCreationRequest creationRequest);
}