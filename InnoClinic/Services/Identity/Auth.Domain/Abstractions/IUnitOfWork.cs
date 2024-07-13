public interface IUnitOfWork : IDisposable
{
    IAccountRepository AccountRepository { get; }
    public Task DeleteDataBaseAsync(CancellationToken cancellationToken = default);
    public Task CreateDataBaseAsync(CancellationToken cancellationToken = default);
    public Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    public Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    public Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    public Task<int> CompleteAsync(CancellationToken cancellationToken = default);
}