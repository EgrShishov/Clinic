public sealed record DownloadAppointmentResultsCommand(int ResultsId) : IRequest<ErrorOr<byte[]>>
{
}