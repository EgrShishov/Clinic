public sealed record UpdateOfficeCommand(Office office) : IRequest<ErrorOr<Unit>>
{
}
