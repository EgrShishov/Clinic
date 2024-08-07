public class FilesHttpClient : IFilesHttpClient
{
    private readonly HttpClient _httpClient;
    public FilesHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<byte[]> GetDocumentForResultAsync(int ResultsId)
    {
        throw new NotImplementedException();
    }
}
