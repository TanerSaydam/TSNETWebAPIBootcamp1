using EFCore.Relationship.Abstractions;

namespace EFCore.Relationship.Models;

public sealed class Invoice : Entity
{
    public DateOnly Date { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public List<InvoiceDetail>? InvoiceDetails { get; set; }
}

public sealed class InvoiceDetail : Entity
{
    public Guid InvoiceId { get; set; }
    public string ItemName { get; set; } = string.Empty;
}
