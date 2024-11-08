namespace EnglishTutorAI.Application.Interfaces;

public interface ISentenceSplitterService
{
    IEnumerable<string> Split(string text);
}