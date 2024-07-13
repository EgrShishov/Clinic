public sealed record UploadPhotoCommand(
    Stream FileStream,
    string FileName,
    string ContentType) : IRequest<ErrorOr<string>>
{
}
