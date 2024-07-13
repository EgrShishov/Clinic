public class UploadPhotoCommandHandler(IFileFacade fileFacade) : IRequestHandler<UploadPhotoCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(UploadPhotoCommand request, CancellationToken cancellationToken)
    {
        var url = await fileFacade.UploadPhotoAsync(request.FileStream, request.FileName, request.ContentType);
        if (string.IsNullOrEmpty(url))
        {
            return Error.NotFound();
        }

        return url;
    }
}
