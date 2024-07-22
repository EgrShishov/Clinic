public sealed record CreateReceptionistCommand(
    string FirstName,
    string LastName,
    string MiddleName,
    string Email,
    string OfficeId,
    byte[] Photo) : IRequest<ErrorOr<CreateReceptionistProfileResponse>>
{
}
