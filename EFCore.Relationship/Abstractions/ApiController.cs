using AutoMapper;
using EFCore.Relationship.Context;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.Relationship.Abstractions;
[Route("api/[controller]/[action]")]
[ApiController]
public abstract class ApiController : ControllerBase
{
    public readonly ApplicationDbContext context;
    public readonly IMapper mapper;

    protected ApiController(ApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }
}
