namespace EFCore.Relationship.Dtos;

public sealed record CategoryDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public List<CategoryProductDto> CategoryProducts { get; init; }

    //public CategoryDto(Guid id, string name, List<Product> products)
    //{
    //    Id = id;
    //    Name = name;
    //    CategoryProducts = products.Select(s => new CategoryProductDto(s.Id, s.Name)).ToList();
    //}
}



public sealed record CategoryProductDto(
    Guid Id,
    string Name);