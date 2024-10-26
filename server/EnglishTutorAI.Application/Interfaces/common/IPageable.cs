namespace EnglishTutorAI.Application.Interfaces.common;

public interface IPageable
{
    int PageNumber { get; init; }

    int PageSize { get; init; }
}