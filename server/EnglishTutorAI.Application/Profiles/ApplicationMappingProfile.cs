using AutoMapper;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EnglishTutorAI.Application.Profiles;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        CreateMap<Document, DocumentResponse>();
        CreateMap<Document, DocumentListItem>()
            .ForMember(d => d.StudyTopic, o => o.MapFrom(s => s.StudyTopic.ToString()));

        CreateMap<ChatMessage, ChatMessageResponse>();
        CreateMap<IdentityUser, IdentityUserResponse>();
    }
}