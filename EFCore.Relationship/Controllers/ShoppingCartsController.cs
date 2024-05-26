using AutoMapper;
using EFCore.Relationship.Abstractions;
using EFCore.Relationship.Context;
using EFCore.Relationship.Dtos;
using EFCore.Relationship.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Relationship.Controllers;
public sealed class ShoppingCartsController : ApiController
{
    public ShoppingCartsController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var shoppingCarts =
            await context.ShoppingCarts
            .Include(p => p.Product)
            .ToListAsync(cancellationToken);

        var newShoppingCarts = mapper.Map<List<ShoppingCartDto>>(shoppingCarts);


        return Ok(newShoppingCarts);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateShoppingCartDto request, CancellationToken cancellationToken)
    {

        Product? product = await context.Products.FindAsync(request.ProductId, cancellationToken);

        if (product is not null)
        {
            if (product.Quantity < request.Quantity)
            {
                return BadRequest(new { Message = "Product quantity is not enough to adding" });
            }
            product.Quantity -= request.Quantity;
        }

        ShoppingCart? shoppingCart = null;
        shoppingCart =
            await context.ShoppingCarts
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ProductId == request.ProductId, cancellationToken);

        if (shoppingCart is not null)
        {
            shoppingCart.Quantity += request.Quantity;
            context.Update(shoppingCart);
        }
        else
        {
            shoppingCart = mapper.Map<ShoppingCart>(request);
            await context.AddAsync(shoppingCart, cancellationToken);
        }

        int result = await context.SaveChangesAsync(cancellationToken);

        if (result == 0)
        {
            return BadRequest(new { Message = "Something went wrong" });
        }

        return Created();
    }
}
