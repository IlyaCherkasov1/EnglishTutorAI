namespace EnglishTutorAI.Application.Interfaces;

public interface IElevenLabsService
{
    Task GenerateSpeechAsync(string text);
}