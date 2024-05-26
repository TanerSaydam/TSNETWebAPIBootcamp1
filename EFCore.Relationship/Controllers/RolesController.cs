using AutoMapper;
using EFCore.Relationship.Abstractions;
using EFCore.Relationship.Context;
using EFCore.Relationship.Dtos;
using EFCore.Relationship.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace EFCore.Relationship.Controllers;

public class RolesController : ApiController
{
    public RolesController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRoleDto request, CancellationToken cancellationToken)
    {
        bool isNameExists = await context.Roles.AnyAsync(p => p.Name == request.Name, cancellationToken);

        if (isNameExists)
        {
            return BadRequest(new { Message = "User already exists" });
        }

        Role role = mapper.Map<Role>(request);

        await context.AddAsync(role, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Created();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var roles =
            await context.Roles
            .Include(p => p.UserRoles)!
            .ThenInclude(p => p.User)
            .ToListAsync(cancellationToken);

        var newRoles = mapper.Map<List<RoleDto>>(roles);

        return Ok(newRoles);
    }
}
