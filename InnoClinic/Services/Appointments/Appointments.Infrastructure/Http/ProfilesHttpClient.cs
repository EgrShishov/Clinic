using System.Net.Http.Json;

public class ProfilesHttpClient : IProfilesHttpClient
{
    private readonly HttpClient _httpClient;

    public ProfilesHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> DoctorExistsAsync(int doctorId)
    {
        var response = await _httpClient.GetAsync($"api/doctors/{doctorId}");
        return response.IsSuccessStatusCode;
    }

    public async Task<DoctorProfileForPatientResponse> GetDoctorAsync(int doctorId)
    {
        var response = await _httpClient.GetAsync($"api/doctors/{doctorId}");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        return await response.Content.ReadFromJsonAsync<DoctorProfileForPatientResponse>();
    }
    public async Task<PatientProfileResponse> GetPatientAsync(int patientId)
    {
        var response = await _httpClient.GetAsync($"api/patients/{patientId}");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<PatientProfileResponse>();
    }

    public async Task<bool> PatientExistsAsync(int patientId)
    {
        var response = await _httpClient.GetAsync($"api/patinets/{patientId}");
        return response.IsSuccessStatusCode;
    }
}
