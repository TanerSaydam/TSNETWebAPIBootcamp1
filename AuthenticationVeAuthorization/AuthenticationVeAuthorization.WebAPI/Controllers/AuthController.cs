using AuthenticationVeAuthorization.WebAPI.DTOs;
using AuthenticationVeAuthorization.WebAPI.Models;
using AuthenticationVeAuthorization.WebAPI.Services;
using AuthenticationVeAuthorization.WebAPI.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationVeAuthorization.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public sealed class AuthController : ControllerBase
{
    private List<User> Users = new();
    private readonly JwtProvider _jwtProvider;
    public AuthController(JwtProvider jwtProvider)
    {
        byte[] passwordSalt, passwordHash;

        HashingHelper.CreatePassword("1", out passwordHash, out passwordSalt);

        User user = new()
        {
            Id = Guid.NewGuid(),
            FirstName = "Taner",
            LastName = "Saydam",
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            UserName = "taner"
        };

        Users.Add(user);
        _jwtProvider = jwtProvider;

    }

    [HttpPost]
    public IActionResult Login(LoginDto request)
    {
        User? user = Users.FirstOrDefault(p => p.UserName == request.UserName);
        if (user is null)
        {
            return BadRequest(new { Message = "User not found" });
        }

        bool checkPassword = HashingHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt);

        if (!checkPassword)
        {
            return BadRequest(new { Message = "Password is wrong" });
        }

        List<string> roles = new()
        {
            "Admin",
            "Moderator",
            "User"
        };

        return Ok(new { Token = _jwtProvider.CreateToken(user, roles) });
    }
}
