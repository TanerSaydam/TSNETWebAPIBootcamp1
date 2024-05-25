namespace EFCore.Middle.MinimalAPI.Models;

public sealed class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    //public ICollection<Product>? Products { get; set; }
}
