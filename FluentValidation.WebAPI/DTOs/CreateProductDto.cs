namespace FluentValidation.WebAPI.DTOs;

public sealed record CreateProductDto(
    string Name,
    string Description,
    decimal Price,
    int Quantity);
