using AutoMapper;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Resolvers;

public class UserDocumentSentencesToDocumentResponseResolver :
    IValueResolver<UserDocument, DocumentDetailsModel, IEnumerable<string>>
{
    public IEnumerable<string> Resolve(
        UserDocument source, DocumentDetailsModel destination, IEnumerable<string> destMember, ResolutionContext context)
    {
        return source.Document.Sentences.OrderBy(s => s.Position).Select(s => s.Text);
    }
}