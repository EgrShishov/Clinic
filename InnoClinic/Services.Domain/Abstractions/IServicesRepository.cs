public interface IServicesRepository
{
    public Task<Service> GetServiceByIdAsync(int id, CancellationToken cancellationToken = default);
    public Task<IEnumerable<Service>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<Service> AddServiceAsync(Service service, CancellationToken cancellationToken = default);
    public Task UpdateServiceAsync(Service service, CancellationToken cancellationToken = default);
}