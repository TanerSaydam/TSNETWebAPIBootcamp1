using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace First.WEBAPI.Controllers;

[Route("api/[controller]/[action]")] //localhost:7101/helloworld/test
[ApiController]
public sealed class HelloWorldController : ControllerBase
{
    [HttpGet("{age}/{name}")] //hangi tipteyse isimsiz olarak sadece 1 tip olarak
    //isim veriyorsak onu unique olacak şekilde vermeliyiz
    public IActionResult Test(int age, string name )//Test2/10/asdasd => router params
    {
        List<int> Ids = new()
        {
            1,2,3,4
        };
        return Ok(new { Message = "API is working..." });
    }

    [HttpGet]//best practices takip etmek sizi daha fazla yazılımcıyla aynı 
    //ortamda çalıştırır
    public IActionResult Test2(int count)//Test2?age=10 => query params
    {
        return NoContent();
    }
}

//SOLID
//Clean Code
// kodun okunabilir olması > kodun kısa olmasından
