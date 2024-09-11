using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetDocuments;

public class GetDocumentsQuery : IRequest<IReadOnlyList<DocumentListItem>>
{
}