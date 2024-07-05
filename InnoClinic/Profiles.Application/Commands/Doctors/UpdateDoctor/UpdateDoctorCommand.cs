
namespace Profiles.Application.Commands.Doctors.UpdateDoctor
{
    public record UpdateDoctorCommand(
        int DoctorId,
        string FirstName,
        string LastName,
        string MiddleName,
        DateTime DateOfBirth,
        int SpecializationId,
        int OfficeId,
        int CareerStartYear,
        ProfileStatus Status
    ) : IRequest<ErrorOr<Doctor>>;
}
