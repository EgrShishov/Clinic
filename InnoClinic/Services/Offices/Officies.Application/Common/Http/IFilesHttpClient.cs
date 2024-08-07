using Microsoft.AspNetCore.Http;

public interface IFilesHttpClient
{
    public Task<ErrorOr<string>> UploadPhoto(IFormFile photo, string fileName);
    public Task<ErrorOr<Unit>> DeletedPhoto(string fileName);
}
