using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using TaskManager.WebAPI.Dtos;
using TaskManager.WebAPI.Models;
using TaskManager.WebAPI.Utilities;

namespace TaskManager.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public sealed class PersonelsController : ControllerBase
{
    public static List<Personel> Personels { get; set; } = new();

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(Personels);
    }

    [HttpPost]
    public IActionResult Create([FromForm] CreatePersonelDto request)
    {
        string fileName = request.File.FileName.CreateFileName();

        using (var stream = System.IO.File.Create($"wwwroot/avatar/{fileName}"))
        {
            request.File.CopyTo(stream);
        }

        Personel personel = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            CreatetAt = DateTime.Now, 
            AvatarUrl = fileName
        };

        Personels.Add(personel);

        return Created();
    }

    [HttpDelete]
    public IActionResult DeleteById(Guid id)
    {
        Personel? personel = Personels.FirstOrDefault(p => p.Id == id);
        if(personel is null)
        {
            return NotFound();
        }

        System.IO.File.Delete($"wwwroot/avatar/{personel.AvatarUrl}");

        Personels.Remove(personel);

        return Ok(new { Message = "Personel kaydı başarıyla silindi" });
    }

    [HttpPut]
    public IActionResult Update([FromForm] UpdatePersonelDto request)
    {
        Personel? personel = 
            Personels
            .FirstOrDefault(p => p.Id == request.Id);

        if(personel is null)
        {
            return BadRequest(new { Message = "Personel bulunamadı" });
        }

        personel.FirstName = request.FirstName;
        personel.LastName = request.LastName;

        if(request.File is not null)
        {
            System.IO.File.Delete($"wwwroot/avatar/{personel.AvatarUrl}");

            string fileName = request.File.FileName.CreateFileName();

            using (var stream = System.IO.File.Create($"wwwroot/avatar/{fileName}"))
            {
                request.File.CopyTo(stream);
            }

            personel.AvatarUrl = fileName;
        }

        return Ok(new { Message = "Personel kaydı başarıyla güncellendi" });
    }
}