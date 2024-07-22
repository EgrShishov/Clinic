using Azure.Storage.Blobs.Models;

public class FileRepository : IFileRepository
{
    private readonly BlobServiceClient _blobServiceClient;

    public FileRepository(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task<string> UploadAsync(Stream stream, string fileName, string contentType, string containerName, CancellationToken cancellationToken = default)
    {
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);

        await blobClient.UploadAsync(
            stream,
            new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = contentType
                }
            },
            cancellationToken);

        return blobClient.Uri.ToString();
    }

    public async Task<FileResponse> DownloadAsync(string fileName, string containerName, CancellationToken cancellationToken = default)
    {
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);

        var downloadResult = await blobClient.DownloadContentAsync(cancellationToken: cancellationToken);

        return new FileResponse(
            downloadResult.Value.Content.ToArray(),
            downloadResult.Value.Details.ContentType,
            blobClient.Name);
    }

    public async Task<string> DeleteAsync(string fileName, string containerName, CancellationToken cancellationToken = default)
    {
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);

        await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);

        return blobClient.Uri.ToString();
    }
}
