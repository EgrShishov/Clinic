public sealed record DownloadPhotoQuery(string fileName) : IRequest<ErrorOr<FileResponse>>
{
}