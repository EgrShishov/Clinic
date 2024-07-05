
namespace InnoClinic.Contracts.Profiles.Patients.Requests
{
    public sealed record UpdatePatientRequest(
        string FirstName,
        string LastName,
        string MiddleName,
        string PhoneNumber,
        DateTime DateOfBirth);
}
