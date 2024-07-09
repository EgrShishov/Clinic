public sealed record CreateDoctorCommand(
    string FirstName, 
    string LastName, 
    string MiddleName,
    DateTime DateOfBirth,
    int SpecializationId,
    int OfficeId,
    int AccountId,
    int CareerStartYear,
    ProfileStatus Status) : IRequest<ErrorOr<Doctor>>
{ }
