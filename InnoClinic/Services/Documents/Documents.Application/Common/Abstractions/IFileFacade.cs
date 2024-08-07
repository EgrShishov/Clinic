using Microsoft.AspNetCore.Http;

public interface IFileFacade
{
    public Task<ErrorOr<string>> UploadDocumentAsync(IFormFile file, int resultId);
    public Task<ErrorOr<string>> UploadPhotoAsync(IFormFile file);
    public Task<ErrorOr<FileResponse>> DownloadDocumentAsync(string fileName);
    public Task<ErrorOr<FileResponse>> DownloadPhotoAsync(string fileName);
    public Task<ErrorOr<Unit>> DeleteDocumentAsync(string fileName);
    public Task<ErrorOr<Unit>> DeletePhotoAsync(string fileName);
}
