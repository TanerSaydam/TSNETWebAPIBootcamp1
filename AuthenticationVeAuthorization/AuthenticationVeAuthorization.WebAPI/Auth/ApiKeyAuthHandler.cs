using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace AuthenticationVeAuthorization.WebAPI.Auth;

public class ApiKeyAuthHandler : AuthenticationHandler<ApiKeyAuthSchemeOptions>
{
    private string apiKey;
    public ApiKeyAuthHandler(
        IOptionsMonitor<ApiKeyAuthSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
        apiKey = options.CurrentValue.ApiKey;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid API Key"));
        }

        var header = Request.Headers[HeaderNames.Authorization].ToString();
        if (header != apiKey)
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid API Key"));
        }

        var claimsIdentity = new ClaimsIdentity(null, apiKey);

        var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), "MyAuthScheme");

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
