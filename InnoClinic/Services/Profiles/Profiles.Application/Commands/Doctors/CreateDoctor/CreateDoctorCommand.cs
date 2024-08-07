using Microsoft.AspNetCore.Http;

public sealed record CreateDoctorCommand(
    string FirstName, 
    string LastName, 
    string MiddleName,
    string Email,
    DateTime DateOfBirth,
    IFormFile Photo,
    int SpecializationId,
    int OfficeId,
    int CareerStartYear,
    int CreatedBy,
    int AccountId,
    ProfileStatus Status) : IRequest<ErrorOr<Doctor>>
{ }
