namespace TaskManager.WebAPI.Dtos;

public sealed record UpdatePersonelDto(
    Guid Id,
    string FirstName,
    string LastName,
    IFormFile? File);