using Microsoft.AspNetCore.Mvc;

namespace First.WEBAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public sealed class PostAPIController : ControllerBase
{
    //Post
    [HttpPost]
    public IActionResult Create(CreateDto request, int age, string name)
    {
        return NoContent();
    }//404 0 500 405 415

    [HttpPut]
    public IActionResult Update()
    {
        return NoContent();
    }

    [HttpDelete]
    public IActionResult Delete()
    {
        return NoContent();
    }
}

public sealed record CreateDto(
    UserDto userDto,
    CategoryDto categoryDto);

public sealed record UserDto(
    int Age,
    string Name,
    string Email
    );

public sealed record CategoryDto(
    string Name);
