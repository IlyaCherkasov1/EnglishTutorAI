using MediatR;

namespace EnglishTutorAI.Application.Handlers.GenerateSentences;

public class GenerateSentencesCommand : IRequest<string>
{
    public string Text { get; }

    public GenerateSentencesCommand(string text)
    {
        Text = text;
    }
}