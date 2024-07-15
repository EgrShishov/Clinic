using Microsoft.AspNetCore.Http;

public sealed record CreatePatientRequest(
    string FirstName,
    string LastName,
    string MiddleName,
    string PhoneNumber,
    DateTime DateOfBirth,
    IFormFile Photo
    );
