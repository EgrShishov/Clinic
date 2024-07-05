namespace Officies.Application.Commands.AddOffice
{
    public sealed record AddOfficeCommand(Office office) : IRequest<ErrorOr<Unit>>
    {
    }
}
