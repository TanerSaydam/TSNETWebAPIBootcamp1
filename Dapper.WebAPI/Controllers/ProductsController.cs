using Dapper.WebAPI.DTOs;
using Dapper.WebAPI.Models;
using Dapper.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dapper.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public sealed class ProductsController(DatabaseService databaseService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var connection = databaseService.GetConnection();

        IEnumerable<Product> products =
            await connection.QueryAsync<Product>("Select * from Products");

        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductDto request)
    {
        var connection = databaseService.GetConnection();
        var sql = "INSERT INTO Products (Name, Price) Values (@Name, @Price)";

        int result = await connection.ExecuteAsync(sql, request);
        if (result > 0)
        {
            return Created();
        }

        return BadRequest(new { Message = "Something went wrong" });

    }


    [HttpPut]
    public async Task<IActionResult> Update(UpdateProductDto request)
    {
        var connection = databaseService.GetConnection();

        var sql = "UPDATE Products SET Name=@Name, Price=@Price where Id=@Id";

        int result = await connection.ExecuteAsync(sql, request);
        if (result > 0)
        {
            return Ok(new { Message = "Update is successful" });
        }

        return BadRequest(new { Message = "Something went wrong" });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var connection = databaseService.GetConnection();

        var sql = "DELETE FROM Products WHERE Id=@Id";

        int result = await connection.ExecuteAsync(sql, new { Id = id });
        if (result > 1)
        {
            return Ok(new { Message = "Delete is successful" });
        }

        return BadRequest(new { Message = "Something went wrong" });
    }
}
