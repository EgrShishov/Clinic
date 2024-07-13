public class ServicesService : IServiceService
{
    private readonly HttpClient _httpClient;

    public ServicesService(HttpClient httpClient)
    {
        _httpClient=httpClient;
    }

    public async Task<bool> ServiceExistsAsync(int serviceId)
    {
        var response = await _httpClient.GetAsync($"api/services/{serviceId}");
        return response.IsSuccessStatusCode;
    }

    public async Task<ServiceInfoResponse> GetServiceAsync(int serviceId)
    {
        //return await _httpClient.GetAsync($"api/services/{serviceId}");
        throw new NotImplementedException();
    }
}
