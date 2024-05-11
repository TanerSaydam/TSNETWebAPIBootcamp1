using eCommerceHomeWork.Abstractions;
using eCommerceHomeWork.DTOs;
using eCommerceHomeWork.Models;
using eCommerceHomeWork.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceHomeWork.Controllers;
public sealed class AuthController : ApiController
{
    private static List<User> Users = new();

    [HttpPost]
    public IActionResult Register(RegisterDto request)
    {
        bool isUserNameExist = Users.Any(p => p.UserName == request.UserName);

        if(isUserNameExist)
        {
            var errorResponse = Result.Failed("Username is already exists");
            return BadRequest(errorResponse);
        }

        User user = new User()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            Password = request.Password
        };

        Users.Add(user);

        return Ok(Result.Succeed("User create is successful"));
    }

    [HttpPost]
    public IActionResult Login(LoginDto request)
    {
        User? user = Users.FirstOrDefault(p => p.UserName == request.UserName);
        if(user is null)
        {
            return BadRequest(Result.Failed("User not found"));
        }

        if(user.Password != request.Password)
        {
            return BadRequest(Result.Failed("Password is wrong"));
        }

        string token = "Token";
        return Ok(Result.Succeed(token));
    }
}
