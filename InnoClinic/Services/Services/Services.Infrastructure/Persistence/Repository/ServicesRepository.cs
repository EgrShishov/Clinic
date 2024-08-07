using System.Linq.Expressions;

public class ServicesRepository : IServicesRepository
{
    private readonly ISqlConnectionFactory _connectionFactory;
    public ServicesRepository(ISqlConnectionFactory connectionFactory) 
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Service> AddServiceAsync(Service service, CancellationToken cancellationToken = default)
    {
        const string query = @"
                INSERT INTO Services (ServiceName, ServiceCategory, ServicePrice, SpecializationId, IsActive)
                VALUES (@{nameof(Service.ServiceName)}, @{nameof(Service.ServiceCategory)}, @{nameof(Service.ServicePrice)},
                    @{nameof(Service.SpecializationId)}, @{nameof(Service.IsActive)})
                RETURNING Id;";

        await using SqlConnection sqlConnection = _connectionFactory.CreateConnection();

        var id = await sqlConnection.QuerySingleAsync<int>(query,
            new
            {
                service.ServicePrice,
                service.SpecializationId,
                service.ServiceCategory,
                service.ServiceName,
                service.IsActive
            });

        service.Id = id;
        return service;
    }

    public async Task<IEnumerable<Service>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        const string query = @"SELECT * FROM Services;";

        await using SqlConnection sqlConnection = _connectionFactory.CreateConnection();

        return await sqlConnection.QueryAsync<Service>(query);
    }

    public Task<IEnumerable<Service>> GetListAsync(Expression<Func<Service, bool>> filter, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Service> GetServiceByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        const string query = @"SELECT * FROM Services WHERE Id = @Id;";

        await using SqlConnection sqlConnection = _connectionFactory.CreateConnection();

        return await sqlConnection.QuerySingleOrDefaultAsync<Service>(query, new
        {
            Id = id
        });
    }

    public async Task UpdateServiceAsync(Service service, CancellationToken cancellationToken = default)
    {
        const string query = @"
            UPDATE Services 
            SET ServiceCategory = @{nameof(Service.ServiceCategory)}, ServiceName = @{nameof(Service.ServiceName)},
                ServicePrice = @{nameof(Service.ServicePrice)}, SpecializationId = @{nameof(Service.SpecializationId)},
                IsActive = @{nameof(Service.IsActive)}
            WHERE Id = @Id;";

        await using SqlConnection sqlConnection = _connectionFactory.CreateConnection();

        await sqlConnection.ExecuteAsync(query, new
        {
            service.ServiceCategory,
            service.ServiceName,
            service.ServicePrice,
            service.SpecializationId,
            service.IsActive,
            service.Id
        });
    }
}
