public interface IUnitOfWork : IDisposable
{
    ISpecializationsRepository Specializations { get; }
    IServicesRepository Services { get; }
    IServiceCategoryRepository Categories { get; }
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
