using EnglishTutorAI.Api.Constants;
using EnglishTutorAI.Application.Constants;
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

            await HandleExceptionAsync(context, traceId);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, string traceId)
    {
        context.Response.ContentType = ContentTypes.Json;
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.Headers.Append(CustomHeaders.ExceptionTraceId, traceId);

        var result = JsonConvert.SerializeObject(new
        {
            message = "Internal server error.",
            traceId
        });

        return context.Response.WriteAsync(result);
    }
}