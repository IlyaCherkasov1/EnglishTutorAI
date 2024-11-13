namespace EnglishTutorAI.Application.Interfaces;

public interface ITextSplitterService
{
    List<string> SplitText(string text);
}