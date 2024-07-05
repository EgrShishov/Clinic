using MongoDB.Driver;

namespace Officies.Domain.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        public IOfficeRepository OfficeRepository { get; }
        public IClientSessionHandle Session { get; }
        public Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        public Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        public Task RollbackTransactionAsync(CancellationToken cancelToken = default);
    }
}
