namespace Officies.Application.Commands.UpdateOffice
{
    public sealed record UpdateOfficeCommand(Office office) : IRequest<ErrorOr<Unit>>
    {
    }
}
