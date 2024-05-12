namespace MinimalAPI.Services;

public sealed class UserService : IUserService
{
    public async Task CreateUserAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("User service is working...");
        //user kayıt işlemi;
        await Task.CompletedTask;
    }
}
