using System.Reflection;
using Autofac;
using Autofac.Builder;
using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Domain.Enums;
using Module = Autofac.Module;

namespace EnglishTutorAI.Infrastructure.DependencyInjection;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assemblies = new List<Assembly>
        {
            Assembly.GetExecutingAssembly(),
            typeof(DependencyAttribute).Assembly,
        };

        var typesWithAttributes = assemblies.SelectMany(a => a.GetTypes())
            .Where(t => t is { IsClass: true, IsAbstract: false })
            .Select(t => new
            {
                Type = t,
                Attribute = t.GetCustomAttributes<DependencyAttribute>(false).FirstOrDefault()
            })
            .Where(t => t.Attribute != null)
            .ToList();

        foreach (var item in typesWithAttributes)
        {
            var type = item.Type;
            var attribute = item.Attribute!;
            var interfaces = type.GetInterfaces();

            if (type.IsGenericTypeDefinition)
            {
                var registrationBuilder = builder.RegisterGeneric(type);

                if (interfaces.Any())
                {
                    var interfaceTypes = interfaces
                        .Where(i => i.IsGenericType)
                        .Select(i => i.GetGenericTypeDefinition())
                        .ToArray();

                    registrationBuilder = registrationBuilder.As(interfaceTypes);
                }

                SetLifetime(registrationBuilder, attribute.Lifetime);
            }
            else
            {
                if (interfaces.Length != 0)
                {
                    var registrationBuilder = builder.RegisterType(type).As(interfaces.First());
                    SetLifetime(registrationBuilder, attribute.Lifetime);
                }
                else
                {
                    var registrationBuilder = builder.RegisterType(type).AsSelf();
                    SetLifetime(registrationBuilder, attribute.Lifetime);
                }
            }
        }
    }

    private static void SetLifetime<TLimit, TActivatorData, TRegistrationStyle>(
        IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registrationBuilder,
        DependencyLifetime lifetime)
    {
        switch (lifetime)
        {
            case DependencyLifetime.Transient:
                registrationBuilder.InstancePerDependency();
                break;
            case DependencyLifetime.Scoped:
                registrationBuilder.InstancePerLifetimeScope();
                break;
            case DependencyLifetime.Singleton:
                registrationBuilder.SingleInstance();
                break;
        }
    }
}