
namespace Profiles.Application.Commands.Doctors.DeleteDoctor
{
    public record DeleteDoctorCommand(int DoctorId) : IRequest<ErrorOr<Unit>>;
}
