namespace EnglishTutorAI.Application.Models.Common;

public class Result<T> : Result
{
    public T? Data { get; init; }
}