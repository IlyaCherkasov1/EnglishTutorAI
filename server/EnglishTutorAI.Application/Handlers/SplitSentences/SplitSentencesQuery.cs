using MediatR;

namespace EnglishTutorAI.Application.Handlers.SplitSentences;

public class SplitSentencesQuery : IRequest<List<string>>
{
    public string Text { get; }

    public SplitSentencesQuery(string text)
    {
        Text = text;
    }
}