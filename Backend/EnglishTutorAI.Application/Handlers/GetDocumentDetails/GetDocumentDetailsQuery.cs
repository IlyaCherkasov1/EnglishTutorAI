using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetDocumentDetails;

public class GetDocumentDetailsQuery : IRequest<DocumentResponse>
{
    public GetDocumentDetailsQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}