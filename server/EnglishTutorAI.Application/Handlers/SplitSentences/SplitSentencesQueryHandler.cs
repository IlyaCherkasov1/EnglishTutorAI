using EnglishTutorAI.Application.Interfaces;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.SplitSentences;

public class SplitSentencesQueryHandler : IRequestHandler<SplitSentencesQuery, IEnumerable<string>>
{
    private readonly ISentenceSplitterService _sentenceSplitterService;

    public SplitSentencesQueryHandler(ISentenceSplitterService sentenceSplitterService)
    {
        _sentenceSplitterService = sentenceSplitterService;
    }

    public Task<IEnumerable<string>> Handle(SplitSentencesQuery request, CancellationToken cancellationToken)
    {
        return _sentenceSplitterService.Split(request.Text);
    }
}