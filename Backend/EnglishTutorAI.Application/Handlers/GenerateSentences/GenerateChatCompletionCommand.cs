using MediatR;

namespace EnglishTutorAI.Application.Handlers.GenerateSentences;

public class GenerateChatCompletionCommand : IRequest<string>
{
    public string Text { get; }

    public GenerateChatCompletionCommand(string text)
    {
        Text = text;
    }
}