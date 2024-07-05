
namespace Profiles.Application.Commands.Patients.DeletePatient
{
    public sealed record DeletePatientCommand(int PatientId) : IRequest<ErrorOr<Unit>>
    {
    }
}
