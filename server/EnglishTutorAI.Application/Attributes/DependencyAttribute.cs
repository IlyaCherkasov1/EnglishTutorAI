using EnglishTutorAI.Domain.Enums;

namespace EnglishTutorAI.Application.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class DependencyAttribute : Attribute
{
    public DependencyLifetime Lifetime { get; }

    public DependencyAttribute(DependencyLifetime lifetime)
    {
        Lifetime = lifetime;
    }
}