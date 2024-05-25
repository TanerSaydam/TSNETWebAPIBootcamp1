using Ardalis.SmartEnum;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Middle.MinimalAPI.Models;

public sealed class Product
{
    public Product()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Money Price { get; set; } = new(0, 1); //Domain-Driven Design => Value Object
    public ProductType Type { get; set; } = ProductType.Sebze;
    [ForeignKey("MyCategory")]
    public int MyCategoryId { get; set; }
    public Category? MyCategory { get; set; }
}

public sealed record Money
{
    private Money()
    {

    }
    public Money(decimal value, int currencyValue)
    {
        //if (value < 1)
        //{
        //    throw new ArgumentException("Price 1 den küçük olamaz");
        //}

        //if (currencyValue != 1 || currencyValue != 2)
        //{
        //    throw new ArgumentException("Sadece TL ya da USD para birimi seçebilirsiniz");
        //}

        Value = value;
        Currency = CurrencyType.FromValue(currencyValue);
    }
    public decimal Value { get; set; }
    public CurrencyType Currency { get; set; } = CurrencyType.TL;
}


//public enum ProductType
//{
//    Sebze = 1,
//    Meyve = 2,
//    Karisik = 3
//}

public sealed class ProductType : SmartEnum<ProductType>
{
    public static ProductType Karisik = new("Sebze & Meyve", 1);
    public static ProductType Meyve = new("Meyve", 2);
    public static ProductType Sebze = new("Sebze", 3);

    public ProductType(string name, int value) : base(name, value)
    {
    }
}

public sealed class CurrencyType : SmartEnum<CurrencyType>
{
    public static CurrencyType TL = new("Türk Lirası", 1);
    public static CurrencyType USD = new("Amerikan Doları", 2);

    public CurrencyType(string name, int value) : base(name, value)
    {
    }
}