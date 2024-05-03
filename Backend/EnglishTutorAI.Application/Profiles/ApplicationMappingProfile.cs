﻿using AutoMapper;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Profiles;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        CreateMap<Story, StoryResponse>();
        CreateMap<Story, StoryListItem>();
    }
}