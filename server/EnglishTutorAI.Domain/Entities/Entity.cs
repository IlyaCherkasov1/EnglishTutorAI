namespace EnglishTutorAI.Domain.Entities;

public class Entity
{
    public Guid Id { get; set; }

    public bool IsNew => Id == default;
}