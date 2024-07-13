public sealed record DownloadDocumentQuery(string fileName) : IRequest<ErrorOr<FileResponse>>
{
}
