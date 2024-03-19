using MediatR;

namespace EnglishTutorAI.Application.Handlers.GenerateSentences;

public class GenerateSentencesCommand : IRequest
{
    public string Phrase { get; }

    public GenerateSentencesCommand(string phrase)
    {
        Phrase = phrase;
    }
}