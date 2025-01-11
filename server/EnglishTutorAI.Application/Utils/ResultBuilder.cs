using EnglishTutorAI.Application.Models.Common;

namespace EnglishTutorAI.Application.Utils;

public static class ResultBuilder
{
    public static Result BuildSucceeded()
    {
        return new Result
        {
            IsSucceeded = true,
        };
    }

    public static Result<T> BuildSucceeded<T>()
    {
        return new Result<T>
        {
            IsSucceeded = true,
        };
    }

    public static Result<T> BuildSucceeded<T>(T data)
    {
        return new Result<T>
        {
            IsSucceeded = true,
            Data = data,
        };
    }

    public static Result BuildFailed(params string[] errors)
    {
        return new Result
        {
            IsSucceeded = false,
            Errors = errors,
        };
    }

    public static Result<T> BuildFailed<T>(params string[] errors)
    {
        return new Result<T>
        {
            IsSucceeded = false,
            Errors = errors,
        };
    }

    public static Result BuildFailed(IEnumerable<string> errors)
    {
        return new Result
        {
            IsSucceeded = false,
            Errors = errors,
        };
    }
}