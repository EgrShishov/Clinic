
namespace InnoClinic.Contracts.Profiles.Patients.Responses
{
    public sealed record PatientProfileResponse(
        int UserId,
        string FirstName,
        string LastName,
        string MiddleName,
        string PhoneNumber,
        DateTime DateOfBirth,
        string PhotoUrl
        );
}
