using EFCore.Middle.MinimalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Middle.MinimalAPI.Context;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}

/*
EF Core da 3 ilişki türü var
1) Bire-bir ilişki
2) Bire-çok ilişki
3) Çoka-çok ilişki

 */