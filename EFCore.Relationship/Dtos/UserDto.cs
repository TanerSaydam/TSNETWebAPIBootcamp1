using EFCore.Relationship.Models;

namespace EFCore.Relationship.Dtos;

public sealed record UserDto
{
    public UserDto(Guid id, string name, List<UserRole> userRoles, Address address)
    {
        Id = id;
        Name = name;
        RoleNames = userRoles.Select(s => s.Role!.Name).ToList();
        Address = address;
    }

    public Guid Id { get; init; }
    public string Name { get; init; }
    public List<string> RoleNames { get; init; }
    public Address Address { get; init; }

};


public sealed record AddressDto(
    string Country,
    string City,
    string FullAddress);