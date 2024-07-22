public sealed record CreateOfficeCommand(
    string City,
    string Street,
    string HouseNumber,
    string OfficeNumber,
    string PhotoId,
    string RegistryPhoneNumber,
    bool IsActive) : IRequest<ErrorOr<Office>>
{
}
