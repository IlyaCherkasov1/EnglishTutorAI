using Amazon.Runtime;
using Amazon.SimpleEmailV2;
using EnglishTutorAI.Api.Interfaces;
using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace EnglishTutorAI.Api.ServiceInstallers;

public class EmailSenderInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        var serviceProvider = services.BuildServiceProvider();
        var awsSettings = serviceProvider.GetRequiredService<IOptions<AwsSettings>>().Value;

        var awsOptions = configuration.GetAWSOptions();
        awsOptions.Credentials = new BasicAWSCredentials(awsSettings.AccessKey, awsSettings.SecretKey);

        services.AddDefaultAWSOptions(awsOptions);
        services.AddAWSService<IAmazonSimpleEmailServiceV2>();
        services.AddTransient<IEmailSender, EmailService>();
    }
}