using FluentValidation.WebAPI.Context;
using FluentValidation.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FluentValidation.WebAPI.Services;

public sealed class ProductService(ApplicationDbContext context)
{
    public async Task<bool> IsNameExistsAsync(string name, CancellationToken cancellationToken = default)
    {
        return await context.Products.AnyAsync(p => p.Name == name, cancellationToken);
    }

    public async Task CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await context.AddAsync(product, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
