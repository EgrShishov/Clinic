public sealed record CreateReceptionistCommand(
    string FirstName,
    string LastName,
    string MiddleName,
    string Email,
    string OfficeId,
    string PhotoUrl) : IRequest<ErrorOr<CreateReceptionistProfileResponse>>
{
}
