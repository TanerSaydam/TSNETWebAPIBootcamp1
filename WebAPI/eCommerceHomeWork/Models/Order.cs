namespace eCommerceHomeWork.Models;

public sealed class Order
{
    public Order()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal ProductPrice { get; set; }    
}
