using EnglishTutorAI.Domain.Enums;

namespace EnglishTutorAI.Application.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class SingletonDependencyAttribute : DependencyAttribute
{
    public SingletonDependencyAttribute() : base(DependencyLifetime.Singleton)
    {
    }
}