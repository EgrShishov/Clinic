using Microsoft.EntityFrameworkCore;

public class DocumentRepository : IDocumentRepository
{
    private readonly DocumentsDbContext _context;

    public DocumentRepository(DocumentsDbContext context)
    {
        _context = context;
    }

    public async Task SaveDocumentAsync(Document document)
    {
        _context.Documents.Add(document);
        await _context.SaveChangesAsync();
    }

    public async Task<Document> GetDocumentByUrlAsync(string url)
    {
        return await _context.Documents.FirstOrDefaultAsync(p => p.Url == url);
    }

    public async Task DeleteDocumentByUrlAsync(string url)
    {
        var document = await _context.Documents.FirstOrDefaultAsync(p => p.Url == url);
        if (document != null)
        {
            _context.Entry(document).State = EntityState.Deleted;
        }
    }
}
