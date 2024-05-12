namespace MinimalAPI.Services;

public interface IUserService
{
    Task CreateUserAsync(CancellationToken cancellationToken = default);
}
