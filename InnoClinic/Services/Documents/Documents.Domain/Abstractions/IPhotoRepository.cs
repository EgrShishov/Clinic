public interface IPhotoRepository
{
    public Task<Photo> GetPhotoAsync(string fileName);
    public Task SavePhotoAsync(Photo photo);
    public Task DeletePhotoAsync(string fileName);
}