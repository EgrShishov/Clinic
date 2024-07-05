
namespace Profiles.Application.Querires.Doctors.FilterBySpecialization
{
    public record FilterBySpecializationQuery(int SpecializationId, int PageNumber, int PageSize) : IRequest<ErrorOr<List<Doctor>>>;

}
