using AutoMapper;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Resolvers;

public class UserTranslateSentencesToTranslatetResponseResolver :
    IValueResolver<UserTranslate, TranslateDetailsModel, IEnumerable<string>>
{
    public IEnumerable<string> Resolve(
        UserTranslate source, TranslateDetailsModel destination, IEnumerable<string> destMember, ResolutionContext context)
    {
        return source.Translate.Sentences.OrderBy(s => s.Position).Select(s => s.Text);
    }
}