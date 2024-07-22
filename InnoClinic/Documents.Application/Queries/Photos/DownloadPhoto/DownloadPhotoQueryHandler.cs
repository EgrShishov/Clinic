public class DownloadPhotoQueryHandler(IFileFacade fileFacade) 
    : IRequestHandler<DownloadPhotoQuery, ErrorOr<FileResponse>>
{
    public async Task<ErrorOr<FileResponse>> Handle(DownloadPhotoQuery request, CancellationToken cancellationToken)
    {
        var response = await fileFacade.DownloadPhotoAsync(request.fileName);
        if (response == null)
        {
            return Error.NotFound();
        }

        return response;
    }
}
