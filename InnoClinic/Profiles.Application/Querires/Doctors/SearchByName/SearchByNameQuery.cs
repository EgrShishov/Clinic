
namespace Profiles.Application.Querires.Doctors.SearchByName
{
    public record SearchByNameQuery(string FirstName, string LastName, string MiddleName) : IRequest<ErrorOr<List<Doctor>>>;

}
