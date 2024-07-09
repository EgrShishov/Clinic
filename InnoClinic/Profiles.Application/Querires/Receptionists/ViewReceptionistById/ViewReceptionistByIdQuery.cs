public sealed record ViewReceptionistByIdQuery(int ReceptionistId) : IRequest<ErrorOr<Receptionist>>
{
}
