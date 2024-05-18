using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System.Net.Mime;

namespace AuthenticationVeAuthorization.WebAPI.Filters;

public sealed class MyAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public async void OnAuthorization(AuthorizationFilterContext context)
    {
        context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues value);
        if (string.IsNullOrEmpty(value) && value != "MySecretSecretKey")
        {
            context.HttpContext.Response.StatusCode = 401;
            context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
            await context.HttpContext.Response.WriteAsync("You are not authorize");
            context.Result = new UnauthorizedResult();
        }
    }
}
