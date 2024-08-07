using Azure.Storage.Blobs.Models;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;

    public BlobStorageService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task<string> UploadAsync(Stream stream, string fileName, string containerName, string contentType, CancellationToken cancellationToken = default)
    {
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);

        await blobClient.UploadAsync(stream,
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

        return new FileResponse
        {
            Content = downloadResult.Value.Content.ToStream(),
            ContentType = downloadResult.Value.Details.ContentType,
            Filename = blobClient.Name
        };
    }

    public async Task<bool> DeleteAsync(string fileName, string containerName, CancellationToken cancellationToken = default)
    {
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);

        var response = await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);

        return response;
    }
}
