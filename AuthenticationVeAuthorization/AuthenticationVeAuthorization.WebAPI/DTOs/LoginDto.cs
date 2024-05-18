namespace AuthenticationVeAuthorization.WebAPI.DTOs;

public sealed record LoginDto(
    string UserName,
    string Password);