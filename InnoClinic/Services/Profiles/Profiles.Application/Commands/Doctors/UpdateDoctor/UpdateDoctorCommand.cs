using Microsoft.AspNetCore.Http;

public record UpdateDoctorCommand(
    int DoctorId,
    string FirstName,
    string LastName,
    string MiddleName,
    DateTime DateOfBirth,
    int SpecializationId,
    int OfficeId,
    int CareerStartYear,
    IFormFile Photo,
    ProfileStatus Status
) : IRequest<ErrorOr<Doctor>>;
