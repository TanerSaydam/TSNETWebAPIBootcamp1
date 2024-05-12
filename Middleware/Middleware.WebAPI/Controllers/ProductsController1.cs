using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Middleware.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public sealed class ProductsController1 : ControllerBase
{
    [HttpGet]
    public IActionResult Calculate()
    {
        //int x = 0;
        //int y = 0;
        //int z = x / y;

        //throw new Exception("Bu bir exception hatası");
        //throw new ArgumentException("Bu bir exception hatası");
        //throw new ArgumentNullException("Bu bir exception hatası");
        //ürün adı daha  önce oluşmuş
        //throw new MyException();

        List<string> names = new() { "Taner", "Mahmut", "Emin" };

        //var result = new Result() { IsSucceed = false, Message = "Bu ürün adı daha önce kullanılmış" };

        //var result = new Result<List<string>>() { Data = names };
        var result1 = Result<string>.Succeed("Ürün kaydı başarıyla tamamlandı");
        var result2 = Result<string>.Failed("Ürün adı daha önce oluşturulmuş", 409);

        throw new ArgumentException("Deneme exception");

        return StatusCode(result1.StatuCode, result1);

    }
}


public class Result<T>
{
    public Result(T data)
    {
        Data = data;
        IsSucceed = true;
        ErrorMessage = null;
    }

    public Result(bool isSucceed, string errorMessage, int statusCode = 500)
    {
        ErrorMessage = errorMessage;
        IsSucceed = isSucceed;
        StatuCode = statusCode;
    }
    public T? Data { get; set; }
    [JsonIgnore]
    public bool IsSucceed { get; set; } = true;
    public string? ErrorMessage { get; set; }

    public int StatuCode { get; set; } = 200;

    public static Result<T> Failed(string errorMessage, int statusCode = 500)
    {
        return new(false, errorMessage, statusCode);
    }

    public static Result<T> Succeed(T data)
    {
        return new(data);
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

}
public class MyException : Exception
{
    public MyException() : base("Bu ürün adı daha önce oluşmuştur")
    {

    }

    public MyException(string message) : base(message)
    {

    }
    public MyException(string message, Exception innerException) : base(message, innerException)
    {

    }
}
