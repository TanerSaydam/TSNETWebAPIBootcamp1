using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace First.WEBAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Example()
    {
        return Ok(new { Message = "API is working..." });
    }
}
