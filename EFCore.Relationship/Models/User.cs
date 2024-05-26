using EFCore.Relationship.Abstractions;

namespace EFCore.Relationship.Models;

public sealed class User : Entity
{
    public string Name { get; set; } = string.Empty;
    public List<UserRole>? UserRoles { get; set; }
    public Address? Address { get; set; }
}

public sealed class Address : Entity
{
    public Guid UserId { get; set; }
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string FullAddress { get; set; } = string.Empty;
}