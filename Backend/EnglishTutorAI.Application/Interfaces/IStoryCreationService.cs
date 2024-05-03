using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Interfaces;

public interface IStoryCreationService
{
    Task AddStory(StoryCreationRequest creationRequest);
}