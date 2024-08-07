public interface IFilesHttpClient
{
    public Task<byte[]> GetDocumentForResultAsync(int ResultsId);
}