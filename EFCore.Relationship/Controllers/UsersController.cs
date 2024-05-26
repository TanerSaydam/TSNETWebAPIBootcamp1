using AutoMapper;
using EFCore.Relationship.Abstractions;
using EFCore.Relationship.Context;
using EFCore.Relationship.Dtos;
using EFCore.Relationship.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace EFCore.Relationship.Controllers;
public class UsersController : ApiController
{
    public UsersController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto request, CancellationToken cancellationToken)
    {
        bool isNameExists = await context.Users.AnyAsync(p => p.Name == request.Name, cancellationToken);

        if (isNameExists)
        {
            return BadRequest(new { Message = "User already exists" });
        }

        User user = mapper.Map<User>(request);

        await context.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Created();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var users =
            await context.Users
            .Include(p => p.UserRoles)!
            .ThenInclude(p => p.Role)
            .Include(p => p.Address)
            .ToListAsync(cancellationToken);

        var newUsers = mapper.Map<List<UserDto>>(users);

        return Ok(newUsers);
    }
}
