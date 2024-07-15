public record UpdateDoctorCommand(
    int DoctorId,
    string FirstName,
    string LastName,
    string MiddleName,
    DateTime DateOfBirth,
    int SpecializationId,
    int OfficeId,
    int CareerStartYear,
    ProfileStatus Status,
    byte[] Photo
) : IRequest<ErrorOr<Doctor>>;
