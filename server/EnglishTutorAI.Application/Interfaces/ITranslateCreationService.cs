using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Interfaces;

public interface ITranslateCreationService
{
    Task AddTranslate(TranslateCreationRequest creationRequest);
}