using Microsoft.EntityFrameworkCore;

public class PhotoRepository : IPhotoRepository
{
    private readonly DocumentsDbContext _context;

    public PhotoRepository(DocumentsDbContext context)
    {
        _context = context;
    }

    public async Task SavePhotoAsync(Photo photo)
    {
        _context.Photos.Add(photo);
        await _context.SaveChangesAsync();
    }

    public async Task<Photo> GetPhotoAsync(string fileName)
    {
        return await _context.Photos.FirstOrDefaultAsync(p => p.Url.EndsWith(fileName));
    }

    public async Task DeletePhotoAsync(string fileName)
    {
        var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Url.EndsWith(fileName));

        if (photo != null)
        {
            _context.Entry(photo).State = EntityState.Deleted;
        }
    }
}
