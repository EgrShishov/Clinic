using MongoDB.Driver;

public interface IUnitOfWork : IDisposable
{
    public IOfficeRepository OfficeRepository { get; }
    public Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    public Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    public Task RollbackTransactionAsync(CancellationToken cancelToken = default);
}
