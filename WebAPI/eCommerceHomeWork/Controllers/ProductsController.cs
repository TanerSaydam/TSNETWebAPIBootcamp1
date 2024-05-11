using eCommerceHomeWork.Abstractions;
using eCommerceHomeWork.DTOs;
using eCommerceHomeWork.Models;
using eCommerceHomeWork.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceHomeWork.Controllers;
public sealed class ProductsController : ApiController
{
    public static List<Product> Products = new();

    [HttpPost]
    public IActionResult Create(CreateProductDto request)
    {
        bool isNameExists = Products.Any(p => p.Name == request.Name);
        if(isNameExists)
        {
            return BadRequest(Result.Failed("This product name is already exists"));
        }

        if(request.Price <= 0)
        {
            return BadRequest(Result.Failed("Product price must be greater then 0"));
        }

        Product product = new()
        {
            Name = request.Name,
            Price = request.Price
        };

        Products.Add(product);

        return Ok(Result.Succeed("Product create is successful"));
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        //db ye bağlan ve verileri çek
        return Ok(Products.OrderBy(p=> p.Name));
    }

    [HttpDelete]
    public IActionResult DeleteById(Guid id)
    {
        Product? product = Products.Find(p=> p.Id == id);

        if(product is null)
        {
            return BadRequest(Result.Failed("Product not found"));
        }

        Products.Remove(product);

        return Ok(Result.Succeed("Product delete is seccessful"));
    }

    [HttpPut]
    public IActionResult Update(UpdateProductDto request)
    {
        Product? product = Products.Find(p => p.Id == request.Id);

        if (product is null)
        {
            return BadRequest(Result.Failed("Product not found"));
        }

        if(product.Name != request.Name)
        {
            bool isNameExists = Products.Any(p => p.Name == request.Name);
            if (isNameExists)
            {
                return BadRequest(Result.Failed("This product name is already exists"));
            }
        }

        product.Name = request.Name;
        product.Price = request.Price;

        return Ok(Result.Succeed("Product update is successful"));
    }
}
