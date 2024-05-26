namespace EFCore.Relationship.Dtos;

public sealed record CreateUserRoleDto(
    Guid UserId,
    Guid RoleId);