public class DownloadFileQueryHandler(IBlobStorageService storageService) : IRequestHandler<DownloadFileQuery, ErrorOr<FileResponse>>
{
    public async Task<ErrorOr<FileResponse>> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
