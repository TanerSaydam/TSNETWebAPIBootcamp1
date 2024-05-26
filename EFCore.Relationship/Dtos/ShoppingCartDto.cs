using EFCore.Relationship.Models;

namespace EFCore.Relationship.Dtos;

public sealed record ShoppingCartDto
{
    public ShoppingCartDto(Guid id, Guid productId, int quantity, Product product)
    {
        Id = id;
        ProductId = productId;
        ProductName = product.Name;
        ProductPrice = product.Price;
        Quantity = quantity;
        TotalPrice = quantity * product.Price;
    }

    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public string ProductName { get; init; }
    public decimal ProductPrice { get; init; }
    public int Quantity { get; init; }
    public decimal TotalPrice { get; init; }
}
