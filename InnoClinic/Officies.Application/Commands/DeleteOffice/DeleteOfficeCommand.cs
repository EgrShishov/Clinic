namespace Officies.Application.Commands.DeleteOffice
{
    public sealed record DeleteOfficeCommand(string Id) : IRequest<ErrorOr<Unit>>
    {
    }
}
