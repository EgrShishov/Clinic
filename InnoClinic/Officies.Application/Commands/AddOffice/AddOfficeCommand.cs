public sealed record AddOfficeCommand(Office office) : IRequest<ErrorOr<Unit>>
{
}
