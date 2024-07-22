using System.Text.Json;

public class ProfileService : IProfileService
{
    private readonly HttpClient _httpClient;

    public ProfileService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> DoctorExistsAsync(int doctorId)
    {
        var response = await _httpClient.GetAsync($"api/doctors/{doctorId}");
        return response.IsSuccessStatusCode;
    }

    public async Task<PatientProfileResponse> GetPatientAsync(int patientId)
    {
        var response = await _httpClient.GetAsync($"api/patients/{patientId}");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<PatientProfileResponse>(responseContent);
    }

    public async Task<bool> PatientExistsAsync(int patientId)
    {
        var response = await _httpClient.GetAsync($"api/patinets/{patientId}");
        return response.IsSuccessStatusCode;
    }
}
