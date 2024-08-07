using Microsoft.Extensions.Configuration;

public class UnitOfWork : IUnitOfWork
{
    private readonly IServiceProvider _serviceProvider;
    public UnitOfWork(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ISpecializationsRepository Specializations => _serviceProvider.GetService<ISpecializationsRepository>();
    public IServicesRepository Services => _serviceProvider.GetService<IServiceProvider>();

    public Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
