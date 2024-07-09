using MongoDB.Driver;

public class UnitOfWork : IUnitOfWork
{
    private readonly MongoClient _client;
    private Lazy<IOfficeRepository> _officeRepository;
    private IClientSessionHandle _session;

    public UnitOfWork(MongoClient client)
    {
        _client = client;
        _officeRepository = new(() => new OfficeRepository(_client));
    }

    public IOfficeRepository OfficeRepository => _officeRepository.Value;
    public IClientSessionHandle Session => _session;

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _session = await _client.StartSessionAsync(cancellationToken: cancellationToken);
        _session.StartTransaction();
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if(_session == null)
        {
            throw new InvalidOperationException("Transaction has not been started");
        }

        try
        {
            await _session.CommitTransactionAsync(cancellationToken: cancellationToken);
        }
        catch
        {
            await _session.AbortTransactionAsync(cancellationToken: cancellationToken);
            throw;
        }
        finally
        {
            _session.Dispose();
            _session = null;
        }
    }
    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_session == null)
        {
            throw new InvalidOperationException("Transaction has not been started");
        }

        try
        {
            await _session.AbortTransactionAsync(cancellationToken);
        }
        finally
        {
            _session.Dispose();
            _session = null;
        }
    }

    public void Dispose()
    {
        _session?.Dispose();
    }
}
