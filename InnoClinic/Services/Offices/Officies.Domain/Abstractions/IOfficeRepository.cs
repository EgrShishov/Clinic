using MongoDB.Driver;
using System.Linq.Expressions;
public interface IOfficeRepository
{
    public Task<List<Office>> GetOfficesAsync();
    public Task<List<Office>> ListOfficesAsync(Expression<Func<Office, bool>> filter);
    public Task<Office> GetOfficeByIdAsync(string id, IClientSessionHandle session);
    public Task AddOfficeAsync(Office office, IClientSessionHandle session);
    public Task UpdateOfficeAsync(Office office, IClientSessionHandle session);
    public Task DeleteOfficeAsync(string id, IClientSessionHandle session);
}
