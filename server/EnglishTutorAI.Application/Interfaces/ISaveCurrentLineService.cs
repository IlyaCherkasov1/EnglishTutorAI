using EnglishTutorAI.Application.Models;

namespace EnglishTutorAI.Application.Interfaces;

public interface ISaveCurrentLineService
{
    Task SaveCurrentLine(SaveCurrentLineRequest request);
}