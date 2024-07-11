using Azure.Storage.Blobs.Models;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly string ContainerName = "Documents";

    public BlobStorageService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task<Guid> UploadAsync(Stream stream, string contentType, CancellationToken cancellationToken = default)
    {
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);

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
    public async Task<FileResponse> DownloadAsync(Guid fileId, CancellationToken cancellationToken = default)
    {
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);

        BlobClient blobClient = blobContainerClient.GetBlobClient(fileId.ToString());

        var downloadResult = await blobClient.DownloadContentAsync(cancellationToken: cancellationToken);
        
        return new FileResponse(downloadResult.Value.Content.ToStream(), downloadResult.Value.Details.ContentType);
    }
    public async Task DeleteAsync(Guid fileId, CancellationToken cancellationToken = default)
    {
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);

        BlobClient blobClient = blobContainerClient.GetBlobClient(fileId.ToString());

        await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }
}