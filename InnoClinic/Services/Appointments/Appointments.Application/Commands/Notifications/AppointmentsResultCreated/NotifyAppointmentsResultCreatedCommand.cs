public sealed record NotifyAppointmentsResultCreatedCommand(int ResultsId) : IRequest<ErrorOr<Unit>>
{
}