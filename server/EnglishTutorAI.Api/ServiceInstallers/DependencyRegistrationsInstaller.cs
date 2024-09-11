using EnglishTutorAI.Api.Interfaces;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Services;
using EnglishTutorAI.Infrastructure.Data;
using EnglishTutorAI.Infrastructure.HttpClients;

namespace EnglishTutorAI.Api.ServiceInstallers;

public class DependencyRegistrationsInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<ITextCorrectionService, TextCorrectionService>();
        services.AddSingleton<IHttpClientFactory, ConfigurableProxyHttpClientFactory>();
        services.AddScoped<IMessageGenerationService, MessageGenerationService>();
        services.AddScoped<IDocumentCreationService, DocumentCreationCreationService>();
        services.AddScoped<IDocumentCounterService, DocumentCounterService>();
        services.AddScoped<IDocumentRetrievalService, DocumentRetrievalService>();
        services.AddScoped<ISentenceSplitterService, SentenceSplitterService>();
        services.AddScoped<IAssistantClient, AssistantClient>();
        services.AddScoped<ISendAssistantMessageService, SendAssistantMessageService>();
        services.AddScoped<ISaveCurrentLineService, SaveCurrentLineService>();
        services.AddScoped<IDeleteDocumentService, DeleteDocumentService>();
        services.AddScoped<ITextComparisonService, TextComparisonService>();
        services.AddScoped<ITextExtractionService, TextExtractionService>();
        services.AddScoped<IChatMessageAddService, ChatMessageAddService>();
        services.AddScoped<ITextCorrectionMessageGenerationService, TextCorrectionMessageGenerationService>();
        services.AddScoped<IGetUserService, GetUserService>();
        services.AddScoped<IIdentityService, IdentityService>();
    }
}