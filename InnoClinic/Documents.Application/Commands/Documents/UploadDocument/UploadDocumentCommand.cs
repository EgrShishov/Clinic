public sealed record UploadDocumentCommand(
    Stream FileStream, 
    string FileName, 
    string ContentType, 
    int ResultId) : IRequest<ErrorOr<string>>
{
}
