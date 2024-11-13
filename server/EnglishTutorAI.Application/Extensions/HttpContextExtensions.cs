using System.ComponentModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace EnglishTutorAI.Application.Extensions;

public static class HttpContextExtensions
{
    public static Guid? GetUserId(this HttpContext httpContext) =>
        httpContext.GetClaim<Guid?>(ClaimTypes.NameIdentifier);

    private static T? GetClaim<T>(this HttpContext httpContext, string claimType)
    {
        if (httpContext.User?.Identity?.IsAuthenticated != true)
        {
            return default;
        }

        var claimValue = httpContext.User.Claims.SingleOrDefault(x => x.Type == claimType)?.Value;

        return !string.IsNullOrEmpty(claimValue)
            ? (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(claimValue)!
            : default;
    }
}