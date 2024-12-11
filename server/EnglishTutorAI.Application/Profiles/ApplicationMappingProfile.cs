using AutoMapper;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Resolvers;
using EnglishTutorAI.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EnglishTutorAI.Application.Profiles;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        CreateMap<UserTranslate, TranslateDetailsModel>()
            .ForMember(d => d.Title, o => o.MapFrom(s => s.Translate.Title))
            .ForMember(d => d.Sentences, o => o.MapFrom<UserTranslateSentencesToTranslatetResponseResolver>())
            .ForMember(d => d.CreatedAt, o => o.MapFrom(s => s.Translate.CreatedAt))
            .ForMember(d => d.CurrentLine, o => o.MapFrom(s => s.CurrentLine))
            .ForMember(d => d.UserTranslateId, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.IsTranslateFinished, o => o.MapFrom(s => s.IsCompleted));

        CreateMap<Translate, TranslateListItem>()
            .ForMember(d => d.StudyTopic, o => o.MapFrom(s => s.StudyTopic.ToString()));

        CreateMap<DialogMessage, ChatMessageResponse>();
        CreateMap<IdentityUser, IdentityUserResponse>();
    }
}