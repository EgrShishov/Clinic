using System.Net.Http;

public class OfficesHttpClient : IOfficesHttpClient
{
    private readonly IHttpClientFactory _factory;
    public OfficesHttpClient(IHttpClientFactory factory)
    {
        _factory = factory;
    }
    public Task CreateOfficeAsync(OfficeModel office)
    {
        throw new NotImplementedException();
    }

    public Task DeleteOfficeAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<OfficeModel> GetOfficeByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OfficeModel>> GetOfficesAsync()
    {
        throw new NotImplementedException();
    }

    public Task UpdateOfficeAsync(OfficeModel office)
    {
        throw new NotImplementedException();
    }
}