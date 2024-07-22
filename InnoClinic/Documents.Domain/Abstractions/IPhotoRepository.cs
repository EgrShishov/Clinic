public interface IPhotoRepository
{
    public Task<Photo> GetPhotoByUrlAsync(string url);
    public Task SavePhotoAsync(Photo photo);
    public Task DeletePhotoByUrlAsync(string url);
}