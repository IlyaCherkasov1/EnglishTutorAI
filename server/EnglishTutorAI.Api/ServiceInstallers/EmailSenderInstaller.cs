using Amazon.Runtime;
using Amazon.SimpleEmailV2;
using EnglishTutorAI.Api.Interfaces;
using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace EnglishTutorAI.Api.ServiceInstallers;

public class EmailSenderInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IEmailService, EmailService>();
    }
}