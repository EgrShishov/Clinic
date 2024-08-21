public interface IGatewayHttpClient
{
    public Task<IEnumerable<OfficeModel>> GetOfficesAsync();
    public Task<OfficeModel> GetOfficeByIdAsync(int id);
    public Task CreateOfficeAsync(OfficeModel office);
    public Task UpdateOfficeAsync(OfficeModel office);
    public Task DeleteOfficeAsync(int id);
}
