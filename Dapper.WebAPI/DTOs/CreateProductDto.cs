namespace Dapper.WebAPI.DTOs;

public sealed record CreateProductDto(
    string Name,
    decimal Price);

//public sealed record CreateProductDto1
//{
//    public CreateProductDto1(string name, decimal price)
//    {
//        Name = name;
//        Price = price;
//    }
//    public string Name { get; init; } = string.Empty;
//    public decimal Price { get; init; }
//}