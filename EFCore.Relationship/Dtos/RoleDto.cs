using EFCore.Relationship.Models;

namespace EFCore.Relationship.Dtos;

public sealed record RoleDto
{
    public RoleDto(Guid id, string name, List<UserRole> userRoles)
    {
        Id = id;
        Name = name;
        UserNames = userRoles.Select(s => s.User!.Name).ToList();
    }

    public Guid Id { get; init; }
    public string Name { get; init; }
    public List<string> UserNames { get; init; }
};