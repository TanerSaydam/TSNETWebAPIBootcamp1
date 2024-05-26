using EFCore.Relationship.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Relationship.Models;

public sealed class ShoppingCart : Entity
{
    [ForeignKey("Product")]
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    public int Quantity { get; set; }
}
