public sealed record DownloadFileQuery(Guid fileId) : IRequest<ErrorOr<FileResponse>>
{
}