using Microsoft.AspNetCore.Http;

public sealed record CreateReceptionistCommand(
    string FirstName,
    string LastName,
    string MiddleName,
    string Email,
    string OfficeId,
    IFormFile Photo) : IRequest<ErrorOr<CreateReceptionistProfileResponse>>
{
}
