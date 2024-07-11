using Azure.Storage.Blobs.Models;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;

    public BlobStorageService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task<Guid?> UploadAsync(Stream stream, string contentType, string containerName, CancellationToken cancellationToken = default)
    {
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        var fileId = Guid.NewGuid();
        BlobClient blobClient = blobContainerClient.GetBlobClient(fileId.ToString());

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
        return fileId;
    }
    public async Task<FileResponse> DownloadAsync(Guid fileId, string containerName, CancellationToken cancellationToken = default)
    {
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        BlobClient blobClient = blobContainerClient.GetBlobClient(fileId.ToString());

        var downloadResult = await blobClient.DownloadContentAsync(cancellationToken: cancellationToken);
        
        return new FileResponse(
            downloadResult.Value.Content.ToStream(), 
            downloadResult.Value.Details.ContentType,
            blobClient.Name);
    }
    public async Task DeleteAsync(Guid fileId, string containerName, CancellationToken cancellationToken = default)
    {
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        BlobClient blobClient = blobContainerClient.GetBlobClient(fileId.ToString());

        await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }
}