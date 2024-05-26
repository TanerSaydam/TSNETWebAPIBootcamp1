namespace EFCore.Relationship.Dtos;

public sealed record ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal KDVPrice { get; set; }
    public string? AvatarUrl { get; set; } = default!;
    public string CategoryName { get; set; } = default!;
}
