using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace First.WEBAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
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
    public IActionResult GetAll()
    {
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
    public IActionResult Delete(int id)
    {
        return NoContent();
    }

}
