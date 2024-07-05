
namespace Profiles.Application.Querires.Patients.ViewById
{
    public sealed record ViewPatientByIdQuery(int PatientId) : IRequest<ErrorOr<Patient>>
    {
    }
}
