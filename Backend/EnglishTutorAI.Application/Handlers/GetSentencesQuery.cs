using EnglishTutorAI.Domain.Entities;
using MediatR;

namespace EnglishTutorAI.Application.Handlers;

public record GetSentencesQuery : IRequest<IReadOnlyList<Sentence>>;