public sealed record CreatePatientCommand(
    string FirstName,
    string LastName,
    string MiddleName,
    string PhoneNumber,
    DateTime DateOfBirth,
    byte[] Photo) : IRequest<ErrorOr<Patient>>
{ }
