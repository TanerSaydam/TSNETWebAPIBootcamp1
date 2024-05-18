namespace AuthenticationVeAuthorization.WebAPI.Models;

public sealed class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public byte[] PasswordSalt { get; set; } = new byte[64];
    public byte[] PasswordHash { get; set; } = new byte[128];
}
