public interface IDocumentRepository
{
    public Task<Document> GetDocumentAsync(string fileName);
    public Task SaveDocumentAsync(Document document);
    public Task DeleteDocumentAsync(string fileName);
}