using EnglishTutorAI.Api.Interfaces;
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
        services.AddSingleton<ITextCorrectionService, TextCorrectionService>();
        services.AddSingleton<IHttpClientFactory, ConfigurableProxyHttpClientFactory>();
        services.AddSingleton<IMessageGenerationService, MessageGenerationService>();
        services.AddScoped<IDocumentCreationService, DocumentCreationCreationService>();
        services.AddScoped<IDocumentCounterService, DocumentCounterService>();
        services.AddScoped<IDocumentRetrievalService, DocumentRetrievalService>();
        services.AddScoped<ISentenceSplitterService, SentenceSplitterService>();
        services.AddSingleton<IAssistantClient, AssistantClient>();
        services.AddSingleton<IAssistantService, AssistantService>();
    }
}