using Microsoft.AspNetCore.Authentication;

namespace AuthenticationVeAuthorization.WebAPI.Auth;

public class ApiKeyAuthSchemeOptions : AuthenticationSchemeOptions
{
    public string ApiKey { get; set; } = "MySecretSecretKey";
}
