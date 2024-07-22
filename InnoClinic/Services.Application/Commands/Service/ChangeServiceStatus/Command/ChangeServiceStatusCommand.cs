public sealed record ChangeServiceStatusCommand(int Id, bool Status) : IRequest<ErrorOr<Service>>
{
}
