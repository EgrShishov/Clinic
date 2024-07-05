
namespace Profiles.Application.Commands.Patients.UpdatePatient
{
    public sealed record UpdatePatientCommand(
        int PatientId,
        string FirstName,
        string LastName,
        string MiddleName,
        string PhoneNumber,
        DateTime DateOfBirth) : IRequest<ErrorOr<Patient>>
    {}
}
