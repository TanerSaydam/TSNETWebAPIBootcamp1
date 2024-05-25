namespace EntityFrameworkCore.WebAPI.DTOs;

public sealed record CreateProductDto(
    string Name,
    decimal Price,
    int Quantity,
    string CategoryName);
