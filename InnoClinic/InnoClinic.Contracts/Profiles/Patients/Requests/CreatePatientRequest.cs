using Microsoft.AspNetCore.Http;

namespace InnoClinic.Contracts.Profiles.Patients.Requests
{
    public sealed record CreatePatientRequest(
        string FirstName,
        string LastName,
        string MiddleName,
        string PhoneNumber,
        DateTime DateOfBirth,
        IFormFile Photo
        );
}
