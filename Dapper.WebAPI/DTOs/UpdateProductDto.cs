namespace Dapper.WebAPI.DTOs;

public sealed record UpdateProductDto(
    int Id,
    string Name,
    decimal Price);
