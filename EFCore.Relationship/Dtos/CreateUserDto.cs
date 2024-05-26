using EFCore.Relationship.Models;

namespace EFCore.Relationship.Dtos;

public sealed record CreateUserDto(
    string Name,
    Address Address);
