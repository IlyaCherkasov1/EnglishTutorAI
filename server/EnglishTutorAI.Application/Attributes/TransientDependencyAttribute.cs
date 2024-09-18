using EnglishTutorAI.Domain.Enums;

namespace EnglishTutorAI.Application.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class TransientDependencyAttribute : DependencyAttribute
{
    public TransientDependencyAttribute() : base(DependencyLifetime.Transient)
    {
    }
}