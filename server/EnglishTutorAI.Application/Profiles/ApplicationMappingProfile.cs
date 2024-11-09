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
        CreateMap<Document, DocumentResponse>()
            .ForMember(d => d.Sentences, o => o.MapFrom<DocumentSentencesToDocumentResponseResolver>());
        CreateMap<Document, DocumentListItem>()
            .ForMember(d => d.StudyTopic, o => o.MapFrom(s => s.StudyTopic.ToString()));

        CreateMap<DialogMessage, ChatMessageResponse>();
        CreateMap<IdentityUser, IdentityUserResponse>();
    }
}