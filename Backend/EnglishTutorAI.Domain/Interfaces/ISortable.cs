using EnglishTutorAI.Domain.Enums;

namespace EnglishTutorAI.Domain.Interfaces;

public interface ISortable
{
    string SortBy { get; }

    SortOrder Direction { get; }
}