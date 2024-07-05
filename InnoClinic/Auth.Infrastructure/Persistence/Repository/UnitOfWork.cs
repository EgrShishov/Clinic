using Auth.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Auth.Infrastructure.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private AuthDbContext _dbContext;
        private Lazy<IAccountRepository> _accountRepository;
        private IDbContextTransaction _transaction;

        public UnitOfWork(AuthDbContext dbContext)
        {
            _dbContext = dbContext;
            _accountRepository = new(() => new AccountRepository(_dbContext));
        }

        public IAccountRepository AccountRepository => _accountRepository.Value;

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            await _transaction.CommitAsync(cancellationToken);
        }

        public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task CreateDataBaseAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.Database.EnsureCreatedAsync(cancellationToken);
        }

        public async Task DeleteDataBaseAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.Database.EnsureDeletedAsync(cancellationToken);
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _dbContext.Dispose();
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            await _transaction.RollbackAsync(cancellationToken);
        }
    }
}
