using EFCore.Relationship.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Relationship.Context;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
    public DbSet<Address> Addresses { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("money");

        modelBuilder.Entity<Category>().HasIndex(x => x.Name).IsUnique();

        modelBuilder.Entity<UserRole>().HasKey(x => new { x.UserId, x.RoleId }); //composite key

        //modelBuilder.Entity<Category>()
        //    .HasMany(p => p.Products)
        //    .WithOne(p => p.Category)
        //    .HasForeignKey(p => p.CategoryId)
        //    .OnDelete(DeleteBehavior.NoAction);

        //modelBuilder.Entity<Product>()
        //    .HasOne(p => p.Category)
        //    .WithMany(p => p.Products)
        //    .HasForeignKey(p => p.CategoryId)
        //    .OnDelete(DeleteBehavior.NoAction);
    }
}
