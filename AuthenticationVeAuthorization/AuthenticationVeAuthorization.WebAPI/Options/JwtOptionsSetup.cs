using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthenticationVeAuthorization.WebAPI.Options;

public sealed class JwtOptionsSetup(IOptionsMonitor<Jwt> jwt) : IPostConfigureOptions<JwtBearerOptions>
{
    public void PostConfigure(string? name, JwtBearerOptions options)
    {
        options.TokenValidationParameters.ValidateIssuer = true;
        options.TokenValidationParameters.ValidateAudience = true;
        options.TokenValidationParameters.ValidateIssuerSigningKey = true;
        options.TokenValidationParameters.ValidateLifetime = true;
        options.TokenValidationParameters.ValidIssuer = jwt.CurrentValue.Issuer;
        options.TokenValidationParameters.ValidAudience = jwt.CurrentValue.Audience;
        options.TokenValidationParameters.IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.CurrentValue.SecretKey));
    }
}
