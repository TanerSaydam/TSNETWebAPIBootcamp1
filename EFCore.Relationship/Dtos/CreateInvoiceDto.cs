namespace EFCore.Relationship.Dtos;

public sealed record CreateInvoiceDto
{
    public DateOnly Date { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public List<CreateInvoiceDetail>? Details { get; set; }
}

public sealed record CreateInvoiceDetail
{
    public string ItemName { get; set; } = string.Empty;
}

