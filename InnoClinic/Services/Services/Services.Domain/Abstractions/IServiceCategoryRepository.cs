public interface IServiceCategoryRepository
{
    public Task<ServiceCategory> GetServiceCategoryByIdAsync(int id, CancellationToken cancellationToken = default);
    public Task<IEnumerable<ServiceCategory>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<ServiceCategory> AddServiceCategoryAsync(ServiceCategory category, CancellationToken cancellationToken = default);
    public Task UpdateServiceCategoryAsync(ServiceCategory category, CancellationToken cancellationToken = default);
}
