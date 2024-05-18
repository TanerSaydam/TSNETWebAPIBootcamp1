using AuthenticationVeAuthorization.WebAPI.Models;
using AuthenticationVeAuthorization.WebAPI.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationVeAuthorization.WebAPI.Services;

public sealed class JwtProvider(
    IConfiguration configuration, //bunu kullanmıyoruz artık 
    IOptionsMonitor<Jwt> jwt)
{
    public string CreateToken(User user, List<string> roles)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim("FullName",string.Join(" ", user.FirstName, user.LastName)),
            new Claim(ClaimTypes.Role, roles[0]) //JsonSerializer.Serialize(roles)
        };

        string securityKeyValue = jwt.CurrentValue.SecretKey;// configuration.GetSection("JWT:SecretKey").Value!;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKeyValue));

        JwtSecurityToken jwtSecurityToken = new(
            issuer: jwt.CurrentValue.Issuer, //configuration.GetSection("JWT:Issuer").Value,
            audience: jwt.CurrentValue.Audience, //configuration.GetSection("JWT:Audience").Value,
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512));


        JwtSecurityTokenHandler handler = new();

        string token = handler.WriteToken(jwtSecurityToken);

        return token;
    }
}
