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
        CreateMap<UserDocument, DocumentDetailsModel>()
            .ForMember(d => d.Title, o => o.MapFrom(s => s.Document.Title))
            .ForMember(d => d.Sentences, o => o.MapFrom<UserDocumentSentencesToDocumentResponseResolver>())
            .ForMember(d => d.CreatedAt, o => o.MapFrom(s => s.Document.CreatedAt))
            .ForMember(d => d.CurrentLine, o => o.MapFrom(s => s.CurrentLine))
            .ForMember(d => d.UserDocumentId, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.IsDocumentFinished, o => o.MapFrom(s => s.IsCompleted));

        CreateMap<Document, DocumentListItem>()
            .ForMember(d => d.StudyTopic, o => o.MapFrom(s => s.StudyTopic.ToString()));

        CreateMap<DialogMessage, ChatMessageResponse>();
        CreateMap<IdentityUser, IdentityUserResponse>();
    }
}