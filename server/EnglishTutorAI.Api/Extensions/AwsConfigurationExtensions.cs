using Amazon.SimpleEmailV2;
using EnglishTutorAI.Api.Constants;

namespace EnglishTutorAI.Api.Extensions;

public static class AwsConfigurationExtensions
{
    public static void ConfigureAwsServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        var awsOptions = configuration.GetAWSOptions();
        services.AddDefaultAWSOptions(awsOptions);
        services.AddAWSService<IAmazonSimpleEmailServiceV2>();

        configuration.AddSystemsManager(source =>
        {
            source.Path = AwsConstants.SystemsManagerPath;
        });
    }
}