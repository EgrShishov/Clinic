public interface IFileRepository
{
    public Task<string> UploadAsync(Stream stream, string fileName, string contentType, string containerName, CancellationToken cancellationToken = default);
    public Task<FileResponse> DownloadAsync(string fileName, string containerName, CancellationToken cancellationToken = default);
    public Task<string> DeleteAsync(string fileName, string containerName, CancellationToken cancellationToken = default);
}
