using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthenticationVeAuthorization.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
public sealed class ProductsController : ControllerBase
{
    [HttpGet]
    //[MyAuthorize]
    [Authorize(Roles = "Admin")]
    public IActionResult GetAll()
    {
        var httpContext = HttpContext;
        var userId = httpContext.User.Claims.First(p => p.Type == ClaimTypes.NameIdentifier).Value;
        return Ok();
    }

    [HttpGet]
    [AllowAnonymous]
    [Authorize(Roles = "Moderator")]
    public IActionResult GetById()
    {
        var httpContext = HttpContext;
        return Ok();
    }
}
