namespace EFCore.Relationship.Dtos;

public sealed record CreateShoppingCartDto(
    Guid ProductId,
    int Quantity);
