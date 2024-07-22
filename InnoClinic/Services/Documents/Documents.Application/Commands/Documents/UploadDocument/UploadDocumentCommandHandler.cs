public class UploadDocumentCommandHandler(IFileFacade fileFacade) 
    : IRequestHandler<UploadDocumentCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
    {
        var url = await fileFacade.UploadDocumentAsync(request.FileStream, request.FileName, request.ContentType, request.ResultId);

        if (string.IsNullOrEmpty(url))
        {
            return Error.NotFound();
        }
        return url;
    }
}
