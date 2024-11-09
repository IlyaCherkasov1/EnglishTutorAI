using AutoMapper;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Resolvers;

public class DocumentSentencesToDocumentResponseResolver :
    IValueResolver<Document, DocumentResponse, IEnumerable<string>>
{
    public IEnumerable<string> Resolve(
        Document source, DocumentResponse destination, IEnumerable<string> destMember, ResolutionContext context)
    {
        return source.Sentences.OrderBy(s => s.Position).Select(s => s.Text);
    }
}