namespace EnglishTutorAI.Application.Interfaces;

public interface ISentenceSplitterService
{
    Task<List<string>> Split(string text);
}