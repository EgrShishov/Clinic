using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace Officies.Infrastructure.Persistence.Repository
{
    public class OfficeRepository : IOfficeRepository
    {
        private readonly IMongoCollection<Office> _offices;

        public OfficeRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("OfficesDb");
            _offices = database.GetCollection<Office>("Offices");
        }

        public async Task<List<Office>> GetOfficesAsync()
        {
            return await _offices.Find(office => true).ToListAsync();
        }

        public async Task<Office> GetOfficeByIdAsync(string id, IClientSessionHandle session)
        {
            return await _offices.Find(session, office => office.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddOfficeAsync(Office office, IClientSessionHandle session)
        {
            await _offices.InsertOneAsync(session, office);
        }

        public async Task UpdateOfficeAsync(Office office, IClientSessionHandle session)
        {
            await _offices.ReplaceOneAsync(session, o => o.Id == office.Id, office);
        }

        public async Task DeleteOfficeAsync(string id, IClientSessionHandle session)
        {
            await _offices.DeleteOneAsync(session, office => office.Id == id);
        }

        public Task<List<Office>> ListOfficesAsync(Expression<Func<Office, bool>> filter)
        {
            var query = _offices.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToListAsync();
        }
    }
}
