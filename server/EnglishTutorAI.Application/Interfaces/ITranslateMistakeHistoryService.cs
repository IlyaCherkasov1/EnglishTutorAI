using EnglishTutorAI.Application.Models.Translates;

namespace EnglishTutorAI.Application.Interfaces;

public interface ITranslateMistakeHistoryService
{
    Task<IEnumerable<TranslateMistakeHistoryItems>> Get(Guid userTranslateId);
}