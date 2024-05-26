using EFCore.Relationship.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Relationship.Models;

public class Product : Entity
{
    public string Name { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string? AvatarUrl { get; set; }
    public byte[]? Avatar { get; set; }
    [ForeignKey("Category")]
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
}