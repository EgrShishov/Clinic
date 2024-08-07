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

    public async Task<Document> GetDocumentAsync(string fileName)
    {
        return await _context.Documents.FirstOrDefaultAsync(p => p.Url.EndsWith(fileName));
    }

    public async Task DeleteDocumentAsync(string fileName)
    {
        var document = await _context.Documents.FirstOrDefaultAsync(p => p.Url.EndsWith(fileName));

        if (document != null)
        {
            _context.Entry(document).State = EntityState.Deleted;
        }
    }
}
