namespace EFCore.Scaffold.MinimalAPI.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }
    public string Value { get; set; } = string.Empty;
}
