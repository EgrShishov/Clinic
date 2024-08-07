using Microsoft.AspNetCore.Http;

public record CreateDoctorRequest(
    IFormFile Photo,
    string FirstName,
    string LastName,
    string MiddleName,
    DateTime DateOfBirth,
    string Email,
    int SpecializationId,
    int OfficeId,
    int CreatedBy,
    DateTime CareerStartYear,
    string Status);
