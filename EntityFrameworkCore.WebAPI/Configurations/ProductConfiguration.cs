using EntityFrameworkCore.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCore.WebAPI.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products", builder =>
        {
            builder.HasCheckConstraint("CK_Product_Quantity_Range", "[Quantity] BETWEEN 1 AND 50000");
            builder.HasCheckConstraint("CK_Product_Name_MinLength", "LEN([Name]) >= 3");
        });
        builder.Property(p => p.Name).IsRequired().HasMaxLength(650).HasColumnType("varchar(650)");
        builder.Property(p => p.CategoryName).IsRequired().HasMaxLength(50).HasColumnType("varchar(50)");
        builder.Property(p => p.Price).IsRequired().HasColumnType("money");
    }
}
