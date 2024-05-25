using EFCore.Middle.MinimalAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Middle.MinimalAPI.Configurations;

public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Name).HasColumnType("varchar(50)");
        builder.Property(p => p.Description).HasColumnType("varchar(150)").IsRequired(false);
        builder.HasIndex(x => x.Name).IsUnique();
        //builder.Property(p => p.Price).HasColumnType("money");

        builder.Property(p => p.Type).HasConversion(type => type.Value, value => ProductType.FromValue(value));
        builder.OwnsOne(p => p.Price, builder =>
        {
            builder.Property(p => p.Value).HasColumnName("Price").HasColumnType("money");
            builder.Property(p => p.Currency).HasColumnName("Currency").HasConversion(currency => currency.Value, value => CurrencyType.FromValue(value));
        });
    }
}
