using EnglishTutorAI.Domain.Enums;
using JetBrains.Annotations;

namespace EnglishTutorAI.Application.Attributes;

[AttributeUsage(AttributeTargets.Class)]
[UsedImplicitly(ImplicitUseKindFlags.Assign)]
public class DependencyAttribute : Attribute
{
    public DependencyLifetime Lifetime { get; }

    public DependencyAttribute(DependencyLifetime lifetime)
    {
        Lifetime = lifetime;
    }
}