namespace TaskManager.WebAPI.Dtos;

public sealed record CreatePersonelDto(
    string FirstName,
    string LastName,
    IFormFile File);
