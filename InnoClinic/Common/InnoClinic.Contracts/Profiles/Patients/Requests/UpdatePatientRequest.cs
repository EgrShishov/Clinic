using Microsoft.AspNetCore.Http;

public sealed record UpdatePatientRequest(
    string FirstName,
    string LastName,
    string MiddleName,
    string PhoneNumber,
    DateTime DateOfBirth,
    IFormFile Photo);
