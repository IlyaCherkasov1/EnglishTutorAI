using EnglishTutorAI.Api.Interfaces;

namespace EnglishTutorAI.Api.Extensions;

internal static class InstallerExtensions
{
    public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
    {
        var installers =
            typeof(Program).Assembly.DefinedTypes
                .Where(type => typeof(IServiceInstaller).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IServiceInstaller>().ToList();

        installers.ForEach(installer => installer.InstallServices(services, configuration));
    }
}