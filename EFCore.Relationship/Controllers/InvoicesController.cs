using AutoMapper;
using EFCore.Relationship.Abstractions;
using EFCore.Relationship.Context;
using EFCore.Relationship.Dtos;
using EFCore.Relationship.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Relationship.Controllers;
public sealed class InvoicesController : ApiController
{
    public InvoicesController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateInvoiceDto request, CancellationToken cancellationToken)
    {
        Invoice invoice = mapper.Map<Invoice>(request);

        await context.AddAsync(invoice, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Created();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var invoices = await context.Invoices.Include(p => p.InvoiceDetails).ToListAsync(cancellationToken);

        return Ok(invoices);
    }
}
