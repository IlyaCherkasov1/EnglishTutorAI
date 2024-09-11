namespace EnglishTutorAI.Application.Models.Common;

public class Result<T> : Result
{
    public Result()
    {
    }

    public T Data { get; init; }
}