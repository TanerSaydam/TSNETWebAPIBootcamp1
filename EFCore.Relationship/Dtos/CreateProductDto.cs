namespace EFCore.Relationship.Dtos;

public sealed record CreateProductDto(
    string Name,
    int Quantity,
    decimal Price,
    Guid CategoryId,
    IFormFile? File);
