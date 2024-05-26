using AutoMapper;
using EFCore.Relationship.Abstractions;
using EFCore.Relationship.Context;
using EFCore.Relationship.Dtos;
using EFCore.Relationship.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Relationship.Controllers;
public sealed class CategoriesController : ApiController
{
    public CategoriesController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var categories =
            await context.Categories
            .Include(p => p.Products)
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);

        var newCategories = mapper.Map<List<CategoryDto>>(categories);

        return Ok(newCategories);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryDto request, CancellationToken cancellationToken)
    {
        bool isNameExists =
            await context.Categories
            .AnyAsync(p => p.Name.ToLower() == request.Name.ToLower(), cancellationToken);

        if (isNameExists)
        {
            return BadRequest(new { Message = "Category name already exists" });
        }

        Category category = new()
        {
            Name = request.Name,
        };

        await context.AddAsync(category, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Created();
    }
}
