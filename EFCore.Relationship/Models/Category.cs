using EFCore.Relationship.Abstractions;

namespace EFCore.Relationship.Models;

public class Category : Entity
{
    public string Name { get; set; } = default!;
    public List<Product>? Products { get; set; }
}
