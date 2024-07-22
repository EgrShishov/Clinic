public interface IServiceService
{
    public Task<bool> ServiceExistsAsync(int serviceId);
    public Task<ServiceInfoResponse> GetServiceAsync(int serviceId);
}