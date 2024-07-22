public class DownloadDocumentQueryHandler(IFileFacade fileFacade)
    : IRequestHandler<DownloadDocumentQuery, ErrorOr<FileResponse>>
{
    public async Task<ErrorOr<FileResponse>> Handle(DownloadDocumentQuery request, CancellationToken cancellationToken)
    {
        var response = await fileFacade.DownloadDocumentAsync(request.fileName);
        if (response == null)
        {
            return Errors.Documents.NotFound;
        }

        return response;
    }
}
