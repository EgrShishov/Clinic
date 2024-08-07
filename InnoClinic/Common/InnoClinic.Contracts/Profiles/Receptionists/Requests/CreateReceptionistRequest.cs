using Microsoft.AspNetCore.Http;

public record CreateReceptionistRequest(
    IFormFile Photo,
    string FirstName,
    string LastName,
    string MiddleName,
    string Email,
    string OfficeId);