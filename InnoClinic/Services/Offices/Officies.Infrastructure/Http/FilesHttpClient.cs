using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

public class FilesHttpClient : IFilesHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    public FilesHttpClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<ErrorOr<Unit>> DeletedPhoto(string fileName)
    {
        var response = await _httpClient.DeleteAsync(fileName);
        if (!response.IsSuccessStatusCode)
        {
            return Error.Failure();
        }
        return Unit.Value;
    }

    public async Task<ErrorOr<string>> UploadPhoto(IFormFile photo, string fileName)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_configuration["DocumentsAPI"]}/file/photo", 
            new UploadPhotoRequest
            {
                file = photo,
            });
        if (!response.IsSuccessStatusCode)
        {
            return Error.Failure();
        }

        return await response.Content.ReadAsStringAsync();
    }
}
