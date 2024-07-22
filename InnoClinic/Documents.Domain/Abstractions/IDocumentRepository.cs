public interface IDocumentRepository
{
    public Task<Document> GetDocumentByUrlAsync(string url);
    public Task SaveDocumentAsync(Document document);
    public Task DeleteDocumentByUrlAsync(string url);
}