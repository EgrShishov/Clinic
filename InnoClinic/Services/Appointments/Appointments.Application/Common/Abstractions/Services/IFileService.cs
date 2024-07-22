public interface IFileService
{
    public Task<byte[]> GetDocumentForResultAsync(int ResultsId);
}