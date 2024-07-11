public interface IBlobStorageService
{
    public Task<Guid?> UploadAsync(Stream stream, string contentType, string containerName, CancellationToken cancellationToken = default);
    public Task<FileResponse> DownloadAsync(Guid fileId, string containerName, CancellationToken cancellationToken = default);
    public Task DeleteAsync(Guid fileId, string containerName, CancellationToken cancellationToken = default);
}