public interface IProfilesHttpClient
{
    public Task<bool> DoctorExistsAsync(int doctorId);
    public Task<bool> PatientExistsAsync(int patientId);
    public Task<PatientProfileResponse> GetPatientAsync(int patientId);
    public Task<DoctorProfileForPatientResponse> GetDoctorAsync(int doctorId);
}
