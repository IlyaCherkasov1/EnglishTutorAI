﻿using EnglishTutorAI.Api.Interfaces;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Services;
using EnglishTutorAI.Application.Services.Sentences;
using EnglishTutorAI.Infrastructure.Data;
using EnglishTutorAI.Infrastructure.HttpClients;

namespace EnglishTutorAI.Api.ServiceInstallers;

public class DependencyRegistrationsInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISentenceRetrieverService, SentenceRetrieverService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IOpenAiService, OpenAiService>();
        services.AddSingleton<IHttpClientFactory, ConfigurableProxyHttpClientFactory>();
        services.AddScoped<IPromptTemplateService, PromptTemplateService>();
        services.AddScoped<IStoryCreationService, StoryCreationCreationService>();
        services.AddScoped<IStoryCounterService, StoryCounterService>();
        services.AddScoped<IStoryRetrievalService, StoryRetrievalService>();
    }
}