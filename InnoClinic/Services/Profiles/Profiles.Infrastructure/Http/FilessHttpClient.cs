using ErrorOr;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

public class FilessHttpClient : IFilesHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    public FilessHttpClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<ErrorOr<Unit>> DeleteDocument(string fileName)
    {
        var response = await _httpClient.DeleteAsync($"{_configuration["DocumentsAPI"]}/file/document/{fileName}");
        if (!response.IsSuccessStatusCode)
        {
            return Error.Failure();
        }
        return Unit.Value;
    }

    public async Task<ErrorOr<Unit>> DeletePhoto(string fileName)
    {
        var response = await _httpClient.DeleteAsync($"{_configuration["DocumentsAPI"]}/file/photo/{fileName}");
        if (!response.IsSuccessStatusCode)
        {
            return Error.Failure();
        }
        return Unit.Value;
    }

    public async Task<ErrorOr<FileResponse>> DownloadDocument(string fileName)
    {
        var response = await _httpClient.GetAsync($"{_configuration["DocumentsAPI"]}/file/document/{fileName}");
        if (!response.IsSuccessStatusCode)
        {
            return Error.Failure();
        }

        using MemoryStream stream = new();
        await response.Content.CopyToAsync(stream);

        var contentType = response.Content.Headers.ContentType?.ToString() ?? "application/octet-stream";

        return new FileResponse(stream.ToArray(), contentType, fileName);
    }

    public Task<ErrorOr<FileResponse>> DownloadPhoto(string photoName)
    {
        throw new NotImplementedException();
    }

    public async Task<ErrorOr<string>> UploadDocument(UploadDocumentRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_configuration["DocumentsAPI"]}/file/document", request);
        if (!response.IsSuccessStatusCode)
        {
            return Error.Failure();
        }

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<ErrorOr<string>> UploadPhoto(UploadPhotoRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_configuration["DocumentsAPI"]}/file/photo", request);
        if (!response.IsSuccessStatusCode)
        {
            return Error.Failure();
        }

        return await response.Content.ReadAsStringAsync();
    }
}
