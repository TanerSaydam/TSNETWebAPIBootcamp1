using FluentValidation.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FluentValidation.WebAPI.Context;

public class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("MyDb");
    }

    public DbSet<Product> Products { get; set; }
}


