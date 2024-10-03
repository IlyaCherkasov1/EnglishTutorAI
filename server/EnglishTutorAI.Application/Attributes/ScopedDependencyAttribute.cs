using EnglishTutorAI.Domain.Enums;

namespace EnglishTutorAI.Application.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ScopedDependencyAttribute : DependencyAttribute
{
    public ScopedDependencyAttribute() : base(DependencyLifetime.Scoped)
    {
    }
}