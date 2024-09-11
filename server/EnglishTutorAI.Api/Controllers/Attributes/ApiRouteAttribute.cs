using EnglishTutorAI.Api.Constants;
using Microsoft.AspNetCore.Mvc;

namespace EnglishTutorAI.Api.Controllers.Attributes;

public class ApiRouteAttribute : RouteAttribute
{
    public ApiRouteAttribute()
        : this("[controller]")
    {
    }

    public ApiRouteAttribute(string route)
        : base($"{Routes.Api}/{route}")
    {
    }
}