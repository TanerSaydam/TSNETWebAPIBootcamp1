using EntityFrameworkCore.WebAPI.Context;
using EntityFrameworkCore.WebAPI.DTOs;
using EntityFrameworkCore.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkCore.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public sealed class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext context;
    public ProductsController()
    {
        context = new();
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        List<Product> products = context.Products.ToList();
        return Ok(products);
    }

    [HttpPost]
    public IActionResult Create(CreateProductDto request)
    {
        Product product = new()
        {
            Name = request.Name,
            Price = request.Price
        }; //auto mapper

        context.Products.Add(product);
        context.SaveChanges();

        return Created();
    }
}
