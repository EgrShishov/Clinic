using Microsoft.Extensions.Configuration;

public class UnitOfWork : IUnitOfWork
{
    private readonly IServiceProvider _serviceProvider;
    public UnitOfWork(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _serviceProvider = serviceProvider;
    }

    public ISpecializationsRepository Specializations => _serviceProvider.GetService<ISpecializationsRepository>();
    public IServicesRepository Services => _serviceProvider.GetService<IServiceProvider>();
    public IServiceCategoryRepository Categories => _serviceProvider.GetService<IServiceCategoryRepository>();

    public Task BeginTransactionAsync()
    {
        throw new NotImplementedException();
    }

    public Task CommitTransactionAsync()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public Task RollbackTransactionAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}
