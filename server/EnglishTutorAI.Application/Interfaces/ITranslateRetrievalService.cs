using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Interfaces;

public interface ITranslateRetrievalService
{
    Task<UserTranslate> GetTranslateDetailsById(Guid translateId);
}