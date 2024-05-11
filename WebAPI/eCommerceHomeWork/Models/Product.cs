﻿namespace eCommerceHomeWork.Models;

public sealed class Product
{
    public Product()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }

}
