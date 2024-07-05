
namespace Profiles.Application.Querires.Patients.SearchPatientByName
{
    public sealed record SearchPatientByNameQuery(string FirstName, string LastName, string MiddleName) : IRequest<ErrorOr<List<Patient>>>
    {
    }
}
