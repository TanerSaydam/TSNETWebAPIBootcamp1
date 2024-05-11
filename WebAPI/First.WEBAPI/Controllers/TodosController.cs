using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace First.WEBAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
//[EnableRateLimiting("fixed")]
public sealed class TodosController : ControllerBase
{
    [HttpPost]
    public IActionResult Create(UserDto request)
    {
        //Test test = new();
        var context = HttpContext;
        return NoContent();
        //secret key 
        //provider key
    }

    [HttpGet]    
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var httpContext = HttpContext; //10:35 görüşelim

        //Task.Run(() =>
        //{
        //    Console.WriteLine("First");
        //}, default);

        //Task.Run(() =>
        //{
        //    Console.WriteLine("Second");
        //}, default);

        //HttpClient httpClient = new();
        //await httpClient.GetAsync("");

        //await Task.Delay(10000);


        await Task.CompletedTask;

        return Ok(new List<string>()
        {
            "Taner","Ahmet","Ayşe"
        });
    }

    [HttpPut] //bu güncelleme işlemlerini temsil ediyor ama illa güncellee yapacaksın diye bir kontrolü yok yani sen burada silme işlemi yapsan uygulama bunu bilemez
    public IActionResult Update()
    {
        return NoContent();
    }

    [HttpDelete("{id}")] //silme işlemleri temsili
    public IActionResult Delete(UserDto userDto, int id)
    {
        return NoContent();
    }

}
