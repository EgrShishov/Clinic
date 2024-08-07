using System.Linq.Expressions;

public class ServiceRepository : IServiceRepository
{
    private readonly AppointmentsDbContext _dbContext;
    public ServiceRepository(AppointmentsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Task<Service> AddServiceAsync(Service service, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Service>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Service>> GetListAsync(Expression<Func<Service, bool>> filter, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Service> GetServiceByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateServiceAsync(Service service, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}