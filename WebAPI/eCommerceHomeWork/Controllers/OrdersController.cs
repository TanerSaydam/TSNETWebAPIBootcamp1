using eCommerceHomeWork.Abstractions;
using eCommerceHomeWork.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceHomeWork.Controllers;
public class OrdersController : ApiController
{
    public static List<Order> Orders = new();

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(Orders);
    }
}
