using AutoMapper;
using EFCore.Relationship.Abstractions;
using EFCore.Relationship.Context;
using EFCore.Relationship.Dtos;
using EFCore.Relationship.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Relationship.Controllers;
public sealed class ProductsController : ApiController
{
    public ProductsController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var products =
            await context.Products
            .Include(p => p.Category)
            .ToListAsync(cancellationToken);

        var newProducts = mapper.Map<List<ProductDto>>(products);

        //var products = (from p in context.Products
        //                join c in context.Categories on p.CategoryId equals c.Id
        //                select new ProductDto
        //                {
        //                    Id = p.Id,
        //                    AvatarUrl = p.AvatarUrl,
        //                    CategoryName = c.Name,
        //                    Name = p.Name,
        //                    Price = p.Price,
        //                    Quantity = p.Quantity
        //                }).ToList();


        return Ok(newProducts);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateProductDto request, CancellationToken cancellationToken)
    {
        string? avatarUrl = default;
        byte[]? avatar = null;

        if (request.File is not null)
        {
            avatarUrl = string.Join("_", DateTime.Now.ToFileTime(), request.File.FileName);
            using (var stream = System.IO.File.Create($"wwwroot/avatars/{avatarUrl}"))
            {
                request.File.CopyTo(stream);
            }

            using (var memoryStream = new MemoryStream())
            {
                request.File.CopyTo(memoryStream);
                avatar = memoryStream.ToArray();
            }
        }

        Product product = mapper.Map<Product>(request);
        product.Avatar = avatar;
        product.AvatarUrl = avatarUrl;

        await context.AddAsync(product, cancellationToken);
        int result = await context.SaveChangesAsync(cancellationToken);

        if (result == 0)
        {
            return BadRequest(new { Message = "Something went wrong" });
        }

        return Created();
    }
}
