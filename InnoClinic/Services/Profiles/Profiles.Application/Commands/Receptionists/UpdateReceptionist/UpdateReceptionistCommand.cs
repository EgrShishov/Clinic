public sealed record UpdateReceptionistCommand(
    int ReceptionistId,
    string FirstName,
    string LastName,
    string MiddleName,
    string Email,
    string OfficeId) : IRequest<ErrorOr<Receptionist>>
{
}
