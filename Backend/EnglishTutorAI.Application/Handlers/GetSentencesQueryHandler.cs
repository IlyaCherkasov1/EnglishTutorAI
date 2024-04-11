using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Domain.Entities;
using MediatR;

namespace EnglishTutorAI.Application.Handlers;

public class GetSentencesQueryHandler : IRequestHandler<GetSentencesQuery, IReadOnlyList<Sentence>>
{
    private readonly ISentenceRetrieverService _sentenceRetrieverService;

    public GetSentencesQueryHandler(ISentenceRetrieverService sentenceRetrieverService)
    {
        _sentenceRetrieverService = sentenceRetrieverService;
    }

    public Task<IReadOnlyList<Sentence>> Handle(GetSentencesQuery request, CancellationToken cancellationToken)
    {
        return _sentenceRetrieverService.Get();
    }
}