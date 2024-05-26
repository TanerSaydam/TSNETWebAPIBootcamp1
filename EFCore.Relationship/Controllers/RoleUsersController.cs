using AutoMapper;
using EFCore.Relationship.Abstractions;
using EFCore.Relationship.Context;
using EFCore.Relationship.Dtos;
using EFCore.Relationship.Models;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.Relationship.Controllers;

public sealed class RoleUsersController : ApiController
{
    public RoleUsersController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserRoleDto request, CancellationToken cancellationToken)
    {
        UserRole userRole = mapper.Map<UserRole>(request);

        await context.AddAsync(userRole);
        await context.SaveChangesAsync(cancellationToken);

        return Created();
    }
}
