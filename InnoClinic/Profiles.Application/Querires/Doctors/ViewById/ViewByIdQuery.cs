
namespace Profiles.Application.Querires.Doctors.ViewById
{
    public record ViewByIdQuery(int DoctorId) : IRequest<ErrorOr<Doctor>>;
}
