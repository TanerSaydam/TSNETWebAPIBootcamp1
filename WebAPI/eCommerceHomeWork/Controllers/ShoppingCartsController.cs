using eCommerceHomeWork.Abstractions;
using eCommerceHomeWork.Models;
using eCommerceHomeWork.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceHomeWork.Controllers;
public sealed class ShoppingCartsController : ApiController
{
    private static List<ShoppingCart> Carts = new();

    [HttpGet]
    public IActionResult Add(Guid productId)
    {
        Product? product = ProductsController.Products.Find(p => p.Id == productId);

        if(product is null)
        {
            return BadRequest(Result.Failed("Product not found"));
        }

        ShoppingCart shoppingCart = new()
        {
            ProductName = product.Name,
            ProductPrice = product.Price
        };

        Carts.Add(shoppingCart);

        return Ok(Result.Succeed("Product has been successfully added to shopping cart"));
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(Carts);
    }

    [HttpGet("{cartOwner}")]
    public IActionResult Pay(string cartOwner)
    {

        List<Order> orders = Carts.Select(s => new Order()
        {
            ProductName = s.ProductName,
            ProductPrice = s.ProductPrice
        }).ToList();

        OrdersController.Orders.AddRange(orders);

        //foreach (var item in Carts)
        //{
        //    Order order = new()
        //    {
        //        ProductName = item.ProductName,
        //        ProductPrice = item.ProductPrice
        //    };
        //    OrdersController.Orders.Add(order);
        //}

        Carts.RemoveRange(0, Carts.Count);

        return Ok(Result.Succeed("Payment is successful"));
       
    }
}
