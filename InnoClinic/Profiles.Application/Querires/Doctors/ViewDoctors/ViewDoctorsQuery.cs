
namespace Profiles.Application.Querires.Doctors.ViewDoctors
{
    public record ViewDoctorsQuery(int PageNumber, int PageSize) : IRequest<ErrorOr<List<Doctor>>>;
}
