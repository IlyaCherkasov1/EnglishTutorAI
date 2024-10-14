namespace EnglishTutorAI.Application.Interfaces;

public interface ISentenceSplitterService
{
    Task<IEnumerable<string>> Split(string text);
}