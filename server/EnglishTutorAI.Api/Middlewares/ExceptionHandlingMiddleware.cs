using EnglishTutorAI.Api.Constants;
using EnglishTutorAI.Application.Constants;
using EnglishTutorAI.Application.Exceptions;
using Newtonsoft.Json;

namespace EnglishTutorAI.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var traceId = Guid.NewGuid().ToString();
            _logger.Log(LogLevel.Error, ex, "TraceId: {traceId}", traceId);

            await HandleExceptionAsync(context, ex, traceId);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception, string traceId)
    {
        context.Response.ContentType = ContentTypes.Json;
        var statusCode = StatusCodes.Status500InternalServerError;
        var message = "Internal server error.";

        if (exception is EntityNotFoundException)
        {
            statusCode = StatusCodes.Status404NotFound;
            message = "The requested entity was not found";
        }

        context.Response.StatusCode = statusCode;
        context.Response.Headers.Append(CustomHeaders.ExceptionTraceId, traceId);

        var result = JsonConvert.SerializeObject(new
        {
            message,
            traceId
        });

        return context.Response.WriteAsync(result);
    }
}