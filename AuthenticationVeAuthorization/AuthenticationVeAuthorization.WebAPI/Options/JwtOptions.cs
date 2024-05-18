using Microsoft.Extensions.Options;

namespace AuthenticationVeAuthorization.WebAPI.Options;

public sealed class JwtOptions(IConfiguration configuration) : IConfigureOptions<Jwt>
{
    public void Configure(Jwt options)
    {
        configuration.GetSection("JWT").Bind(options);
    }
}
