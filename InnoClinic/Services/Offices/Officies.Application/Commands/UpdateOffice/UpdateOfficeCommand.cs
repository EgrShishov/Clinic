public sealed record UpdateOfficeCommand(
    string OfficeId,
    string City,
    string Street,
    string HouseNumber,
    string OfficeNumber,
    string PhotoId,
    string RegistryPhoneNumber,
    bool IsActive) : IRequest<ErrorOr<Unit>>
{
}
