public interface IFileFacade
{
    public Task<string> UploadDocumentAsync(Stream fileStream, string fileName, string contentType, int resultId);
    public Task<FileResponse> DownloadDocumentAsync(string fileName);
    public Task DeleteDocumentAsync(string fileName);
    public Task<string> UploadPhotoAsync(Stream fileStream, string fileName, string contentType);
    public Task<FileResponse> DownloadPhotoAsync(string fileName);
    public Task DeletePhotoAsync(string fileName);
}
