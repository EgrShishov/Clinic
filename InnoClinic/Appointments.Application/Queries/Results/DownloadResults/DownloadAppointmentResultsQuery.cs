public sealed record DownloadAppointmentResultsQuery(int ResultsId) : IRequest<ErrorOr<byte[]>>
{
}