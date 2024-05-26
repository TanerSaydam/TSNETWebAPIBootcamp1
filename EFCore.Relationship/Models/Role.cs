using EFCore.Relationship.Abstractions;

namespace EFCore.Relationship.Models;

public sealed class Role : Entity
{
    public string Name { get; set; } = string.Empty;
    public List<UserRole>? UserRoles { get; set; }
}
